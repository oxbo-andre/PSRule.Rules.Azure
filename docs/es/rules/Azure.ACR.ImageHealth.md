---
severity: Critico
pillar: Seguridad
category: Aplicaciones y servicios
resource: Container Registry
online version: https://azure.github.io/PSRule.Rules.Azure/es/rules/Azure.ACR.ImageHealth/
---

# Eliminar imágenes de contenedores vulnerables

## Sinopsis

Eliminar imágenes de contenedores con vulnerabilidades conocidas.

## Descripción

Cuando Microsoft Defender para registros de contenedores está habilitado, Microsoft Defender analiza las imágenes de contenedores.
Las imágenes de contenedores se escanean en busca de vulnerabilidades conocidas y se marcan como saludables o no saludables.
No se deben utilizar imágenes de contenedores vulnerables.

## Recomendación

Considere usar la eliminación de imágenes de contenedores con vulnerabilidades conocidas.

## Notas

Esta regla se aplica cuando se analizan los recursos implementados en Azure.

## Enlaces

- [Introducción a Microsoft Defender para registros de contenedor](https://docs.microsoft.com/azure/security-center/defender-for-container-registries-introduction)
- [Introducción a Microsoft Defender for Containers](https://docs.microsoft.com/azure/defender-for-cloud/defender-for-containers-introduction)
- [Proteger las imágenes y el tiempo de ejecución](https://docs.microsoft.com/azure/aks/operator-best-practices-container-image-management#secure-the-images-and-run-time)
