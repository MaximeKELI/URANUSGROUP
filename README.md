# URANUS GROUP - Site Web Professionnel

Un site web moderne et professionnel pour URANUS GROUP, développé avec HTML, CSS, JavaScript et C# ASP.NET Core.

## 🚀 Fonctionnalités

### Frontend
- **Design Responsive** : Adapté à tous les écrans (desktop, tablette, mobile)
- **Animations Avancées** : Effets visuels modernes avec AOS et animations personnalisées
- **Interface Moderne** : Design inspiré des meilleures pratiques UX/UI
- **Performance Optimisée** : Chargement rapide et expérience utilisateur fluide
- **Accessibilité** : Respect des standards d'accessibilité web

### Backend
- **API REST** : Endpoints complets pour toutes les fonctionnalités
- **Base de Données** : Entity Framework Core avec SQL Server
- **Validation** : FluentValidation pour la validation des données
- **Logging** : Serilog pour le logging avancé
- **Email** : Intégration SendGrid pour l'envoi d'emails
- **Sécurité** : Middleware de gestion d'erreurs et validation

## 🛠️ Technologies Utilisées

### Frontend
- HTML5
- CSS3 (Variables CSS, Flexbox, Grid)
- JavaScript ES6+
- AOS (Animate On Scroll)
- Font Awesome Icons
- Google Fonts

### Backend
- C# 8.0
- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- AutoMapper
- FluentValidation
- Serilog
- SendGrid

## 📁 Structure du Projet

```
NEW_URANUSGROUP/
├── frontend/
│   ├── css/
│   │   └── style.css          # Styles principaux
│   ├── js/
│   │   ├── main.js           # Fonctionnalités principales
│   │   ├── animations.js     # Animations avancées
│   │   └── api.js            # Intégration API
│   ├── images/               # Images et assets
│   ├── fonts/                # Polices personnalisées
│   └── index.html            # Page principale
└── backend/
    ├── Controllers/          # Contrôleurs API
    ├── Models/              # Modèles de données
    ├── Services/            # Services métier
    ├── Data/               # Contexte de base de données
    ├── DTOs/               # Objets de transfert de données
    ├── Validators/         # Validateurs FluentValidation
    ├── Middleware/         # Middleware personnalisé
    ├── Program.cs          # Point d'entrée de l'application
    └── appsettings.json    # Configuration
```

## 🚀 Installation et Démarrage

### Prérequis
- .NET 8.0 SDK
- SQL Server (LocalDB ou Express)
- Node.js (optionnel pour le développement frontend)

### Backend

1. **Naviguer vers le dossier backend**
   ```bash
   cd backend
   ```

2. **Restaurer les packages NuGet**
   ```bash
   dotnet restore
   ```

3. **Configurer la base de données**
   - Modifier la chaîne de connexion dans `appsettings.json`
   - Créer la base de données :
   ```bash
   dotnet ef database update
   ```

4. **Configurer SendGrid (optionnel)**
   - Obtenir une clé API SendGrid
   - Modifier `appsettings.json` avec votre clé API

5. **Lancer l'application**
   ```bash
   dotnet run
   ```

L'API sera accessible sur `https://localhost:7000` et `http://localhost:5000`

### Frontend

1. **Ouvrir le fichier index.html**
   - Ouvrir `frontend/index.html` dans un navigateur
   - Ou utiliser un serveur local (Live Server, etc.)

2. **Configuration API (optionnel)**
   - Modifier l'URL de base dans `js/api.js` si nécessaire

## 📋 Fonctionnalités Détaillées

### Pages Principales
- **Accueil** : Hero section avec animations, présentation des services
- **Services** : Grille de services avec cartes interactives
- **À Propos** : Statistiques animées, présentation de l'entreprise
- **Solutions** : Onglets interactifs avec démonstrations techniques
- **Contact** : Formulaire de contact avec validation

### Animations et Effets
- **Scroll Animations** : AOS pour les animations au scroll
- **Hover Effects** : Effets 3D sur les cartes de services
- **Parallax** : Effets de parallaxe sur les éléments
- **Particles** : Système de particules animées
- **Typing Animation** : Animation de frappe pour les textes
- **Counter Animation** : Compteurs animés pour les statistiques

### API Endpoints

#### Contact
- `POST /api/contact` - Créer un nouveau contact
- `GET /api/contact` - Récupérer tous les contacts
- `GET /api/contact/{id}` - Récupérer un contact par ID
- `PUT /api/contact/{id}` - Mettre à jour un contact
- `DELETE /api/contact/{id}` - Supprimer un contact
- `PATCH /api/contact/{id}/read` - Marquer comme lu
- `POST /api/contact/{id}/respond` - Répondre à un contact

#### Services
- `GET /api/service` - Récupérer tous les services
- `GET /api/service/{id}` - Récupérer un service par ID
- `GET /api/service/category/{category}` - Récupérer par catégorie
- `POST /api/service` - Créer un service
- `PUT /api/service/{id}` - Mettre à jour un service
- `DELETE /api/service/{id}` - Supprimer un service

#### Newsletter
- `POST /api/newsletter/subscribe` - S'abonner à la newsletter
- `POST /api/newsletter/unsubscribe` - Se désabonner
- `GET /api/newsletter` - Récupérer tous les abonnés
- `GET /api/newsletter/check/{email}` - Vérifier l'abonnement

## 🎨 Personnalisation

### Couleurs
Les couleurs sont définies dans les variables CSS (`:root`) dans `style.css` :
```css
:root {
    --primary-color: #6366f1;
    --secondary-color: #06b6d4;
    --accent-color: #f59e0b;
    /* ... */
}
```

### Animations
Les animations personnalisées sont dans `animations.js` et peuvent être facilement modifiées.

### Contenu
Le contenu des services est géré via la base de données et peut être modifié via l'API ou directement en base.

## 🔧 Configuration

### Base de Données
La base de données est configurée avec Entity Framework Core. Les migrations sont automatiquement appliquées au démarrage.

### Email
Configurez SendGrid dans `appsettings.json` :
```json
{
  "SendGrid": {
    "ApiKey": "YOUR_SENDGRID_API_KEY",
    "FromEmail": "noreply@uranusgroup.com",
    "FromName": "URANUS GROUP"
  }
}
```

### Logging
Les logs sont configurés avec Serilog et écrits dans le dossier `logs/`.

## 📱 Responsive Design

Le site est entièrement responsive avec des breakpoints pour :
- Desktop (1200px+)
- Tablet (768px - 1199px)
- Mobile (320px - 767px)

## 🚀 Déploiement

### Backend
1. Publier l'application :
   ```bash
   dotnet publish -c Release
   ```

2. Déployer sur votre serveur (IIS, Azure, AWS, etc.)

### Frontend
1. Copier les fichiers frontend sur votre serveur web
2. Configurer l'URL de l'API dans `js/api.js`

## 🤝 Contribution

1. Fork le projet
2. Créer une branche feature (`git checkout -b feature/AmazingFeature`)
3. Commit les changements (`git commit -m 'Add some AmazingFeature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrir une Pull Request

## 📄 Licence

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.

## 📞 Support

Pour toute question ou support, contactez-nous :
- Email : contact@uranusgroup.com
- Site web : https://uranusgroup.com

---

**URANUS GROUP** - Solutions Technologiques Innovantes
# URANUSGROUP
