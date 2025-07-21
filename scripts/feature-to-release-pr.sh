#!/bin/bash

# Script para gestionar el Pull Request Feature → Release
# Repositorio: ARS-Avalanche
# Propósito: Integración de cambios desde feature hacia release

set -e

echo "🚀 ARS Avalanche - Feature to Release Integration Script"
echo "======================================================="

# Colores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Función para logging
log_info() {
    echo -e "${BLUE}[INFO]${NC} $1"
}

log_success() {
    echo -e "${GREEN}[SUCCESS]${NC} $1"
}

log_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

log_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Verificar estado del repositorio
check_repo_status() {
    log_info "Verificando estado del repositorio..."
    
    if [ ! -d ".git" ]; then
        log_error "No se encuentra un repositorio Git en el directorio actual"
        exit 1
    fi
    
    if [ -n "$(git status --porcelain)" ]; then
        log_warning "Hay cambios sin confirmar en el repositorio"
        git status --short
        read -p "¿Desea continuar? (y/N): " -n 1 -r
        echo
        if [[ ! $REPLY =~ ^[Yy]$ ]]; then
            log_info "Operación cancelada por el usuario"
            exit 1
        fi
    fi
    
    log_success "Estado del repositorio verificado"
}

# Actualizar ramas
update_branches() {
    log_info "Actualizando ramas desde remoto..."
    
    # Intentar fetch (puede fallar por autenticación)
    if git fetch --all 2>/dev/null; then
        log_success "Ramas actualizadas desde remoto"
    else
        log_warning "No se pudo conectar con el remoto, trabajando con estado local"
    fi
}

# Preparar rama release
prepare_release_branch() {
    log_info "Preparando rama release..."
    
    # Verificar si existe la rama release
    if git show-ref --verify --quiet refs/heads/release; then
        log_info "Rama release existe, cambiando a ella"
        git checkout release
    else
        log_info "Creando rama release desde main/master"
        # Intentar desde main, luego master, luego HEAD
        if git show-ref --verify --quiet refs/heads/main; then
            git checkout -b release main
        elif git show-ref --verify --quiet refs/heads/master; then
            git checkout -b release master
        else
            git checkout -b release HEAD
        fi
        log_success "Rama release creada"
    fi
}

# Preparar rama feature
prepare_feature_branch() {
    log_info "Verificando rama feature..."
    
    if git show-ref --verify --quiet refs/heads/feature; then
        log_success "Rama feature encontrada"
        git checkout feature
        
        # Mostrar últimos commits de feature
        log_info "Últimos cambios en feature:"
        git log --oneline -5 feature
    else
        log_error "Rama feature no encontrada"
        log_info "Creando rama feature desde el estado actual..."
        git checkout -b feature
        log_warning "Rama feature creada desde HEAD actual"
    fi
}

# Verificar diferencias entre ramas
check_differences() {
    log_info "Verificando diferencias entre feature y release..."
    
    # Cambios que feature tiene que release no tiene
    FEATURE_COMMITS=$(git rev-list release..feature --count 2>/dev/null || echo "0")
    
    if [ "$FEATURE_COMMITS" -gt 0 ]; then
        log_info "Feature tiene $FEATURE_COMMITS commits adelante de release"
        log_info "Commits a integrar:"
        git log --oneline release..feature
    else
        log_warning "No hay commits nuevos en feature para integrar"
    fi
    
    # Verificar archivos modificados
    if git diff --quiet release...feature; then
        log_warning "No hay diferencias de archivos entre las ramas"
    else
        log_info "Archivos que serán afectados:"
        git diff --name-status release...feature | head -20
        if [ "$(git diff --name-status release...feature | wc -l)" -gt 20 ]; then
            log_info "... y $(( $(git diff --name-status release...feature | wc -l) - 20 )) archivos más"
        fi
    fi
}

# Crear el merge (simulado)
prepare_merge() {
    log_info "Preparando integración feature → release..."
    
    git checkout release
    
    # Verificar si hay conflictos potenciales
    if git merge-tree $(git merge-base feature release) feature release | grep -q "<<<<<<< "; then
        log_warning "Se detectaron posibles conflictos de merge"
        log_info "Se recomienda revisar manualmente antes de proceder"
    else
        log_success "No se detectaron conflictos obvios"
    fi
    
    log_info "Para completar el merge, ejecute:"
    echo -e "${YELLOW}git merge feature --no-ff -m 'Merge branch feature into release - Integration for next deployment'${NC}"
}

# Generar información para el PR
generate_pr_info() {
    log_info "Generando información para el Pull Request..."
    
    cat << EOF

📋 INFORMACIÓN PARA EL PULL REQUEST
===================================

Título sugerido:
🔄 Integración Feature → Release - $(date +%Y-%m-%d)

Descripción:
Integración de todos los cambios recientes desde la rama feature hacia release.

Commits incluidos: $FEATURE_COMMITS
Archivos modificados: $(git diff --name-only release...feature | wc -l)

Comando para crear el PR (GitHub CLI):
gh pr create --base release --head feature --title "🔄 Integración Feature → Release - $(date +%Y-%m-%d)" --body-file PULL_REQUEST_TEMPLATE.md

EOF
}

# Función principal
main() {
    echo
    log_info "Iniciando proceso de integración Feature → Release"
    echo
    
    check_repo_status
    update_branches
    prepare_release_branch
    prepare_feature_branch
    check_differences
    prepare_merge
    generate_pr_info
    
    echo
    log_success "✅ Preparación completada!"
    log_info "Revise la información generada y proceda con la creación del PR"
    echo
}

# Ejecutar función principal
main "$@"