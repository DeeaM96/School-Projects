# Wiki - Branches
## Description
Le projet GitHub dispose d'une structure de branches afin de permettre à chaque individu de travailler dans un environnement contrôlé, permettant d'analyser et passer
en revue le code avant que celui-ci ne soit déployer de manière définitive.

## Structure de branches
3 branches principales sont présentes dans le projet :
- main
- develop
- feature/

La hiérarchie des branches est la suivante : feature/* > develop > main

### `main`
Branche principale du projet. **Il est interdit en tout temps d'effectuer des changements directement dans cette branche**. Cette branche représente le projet fini
et fonctionnel. Un utilisateur désirant utiliser le code le télécharge à partir de la branche main

### `develop`
Branche de développement du projet. Cette branche sert à contenir les changements reliés à l'implémentation de fonctionnalités du projet ou à la correction de bug.
Une fois les fonctionnalités implémentés et testés, cette branche sera éventuellement fusionner dans la branche main.

### `feature/`
Les branches feature/ sont un regroupement de branche relié à l'implémentation de chaque tâche du projet. normalement, une tâche est relié à une branche feature/.
C'est dans cette branche que le développeur écrit le code et effectue des tests. La branche doit être appelée avec un nom significatif représentant la tâche/problème à
implémenter.

Exemple : feature/2.2-métrique_pull-request_nbs_totals_pr

La branche feature/ sera éventuellement fusionner dans la branche develop


## Création d'une nouvelle branche

### À l'aide du terminal Git Bash 
Ce projet utilise Git Flow. la création de branche doit ce faire avec Git Flow

1. Ouvrir Git Bash et aller au répertoire contenant le code
2. Aller à la branche `develop` : `git checkout develop`
3. Créer une nouvelle tâche `feature/` : `git flow feature start <NOM DU FEATURE>`


