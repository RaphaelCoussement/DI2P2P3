# DI2P2P3 - Password Manager

  

## Contexte

Le projet **DI2P2P3** est une solution permettant de créer des applications et de gérer des mots de passe associés à celles-ci. Le backend est une API Web construite avec **C#** utilisant **Entity Framework Core (EF Core)** pour la gestion des données, tandis que le frontend est une application **Angular** permettant aux utilisateurs d'interagir avec l'API.

  

## Prérequis

Avant de commencer l'installation, assurez-vous que les outils suivants sont installés :

  

- [Node.js](https://nodejs.org/) (pour Angular)

- [npm](https://www.npmjs.com/) (gestionnaire de paquets pour Node.js)

- [.NET SDK](https://dotnet.microsoft.com/download) (pour le projet Web API)

- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) ou une autre base de données compatible avec EF Core

- [Angular CLI](https://angular.io/cli) (facultatif, mais recommandé pour gérer Angular)

  

## Installation

  

### 1. Clonez le dépôt

  

Clonez le projet sur votre machine locale :

    git  clonehttps://github.com/RaphaelCoussement/DI2P2P3.git
    
    cd  DI2P2P3


### 2. Configurez l'API

ouvrez le fichier ```appsettings.json``` pour voir la configuration de la base de données

si vous avez EF Core sur votre machine, vous pouvez faire la migration

```dotnet ef database update```

ensuite, vous pouvez run l'api 

```dotnet run```


### 2. Configurez le front


```cd password-manager```

```npm install```

pour lancer l'application : 

```ng serve```

> ⚠️ **WARNING**  
> Regardez le port de l'API. Peut etre qu'il faudra le changer dans le fichier d'environnement d'angular

