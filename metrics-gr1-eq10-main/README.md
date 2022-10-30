# Oxygène Software - metrics-gr1-eq10

## À propos
metrics-gr1-eq10 est un outil simple permettant d'afficher différentes métriques de [projets GitHub](https://docs.github.com/en/issues/organizing-your-work-with-project-boards/managing-project-boards/about-project-boards). Cet outil, qui est utilisé en console, est divisé en 3 parties:
1. Métriques Kanban
2. Métriques Pull-Request
3. Métriques intégration continue

Chaque métrique permet d'avoir une donnée spécifique relié à un projet GitHub.

## Installation et exécution de metrics-gr1-eq10

### Exigences logicielles
- Windows (10 et ^)
- Python (3.10 et ^)
- Un IDE python (ex. : PyCharm)

### Instructions d'installation
1. Télécharger ou clôner le répertoire
2. Installer les packages requis (Windows : `py -m pip install -r requirements.txt`)


**Pour lancer l'application**
1. Exécuter la classe main.py

## Métriques supportées

### Métriques Kanban
|No | Description                                                    | Justification                                                           |
|---|----------------------------------------------------------------|-------------------------------------------------------------------------|
|1. | Lead time pour chaque tâche  | Affiche le lead time pour chaque tâche. |
|2. | Nombre de tâche(s) dans chaque colonne | Affiche le nombre de tâche(s) active(s) dans chaque colonne du tableau Kanban |
|3. | Temps moyens dans chaque colonne | Affiche le temps moyens que les tâches restent dans chaque colonne du tableau Kanban. |
|4. |  Nombre de tâche complété par jour | Affiche le nombre de tâche déplacé dans la colonne "Done" pour chaque journée |


### Métriques « pull-request »
|No | Description                                                    | Justification                                                           |
|---|----------------------------------------------------------------|-------------------------------------------------------------------------|
|1. | Le nombre tot de pull request  | Le nombre de pull request effectué dans le projet |
|2. | Temps moyen de tout les pull request | Affiche la moyenne de temps de traitement des pull request |
|3. | Temps de traitement d'un pull request | Le temps de traitement qu'un pull request a pris pour être réalisé. |
|4. | Le nombre moyen de lignes de code modifiées | La moyenne des lignes de code modifées pour tout les pull request  |
|5. | Le nombre de lignes de codes modifiées par pull request | les lignes modifiés pour 1 pull request |


### Métrique d'integration continue
|No | Description                                                    | Justification                                                           |
|---|----------------------------------------------------------------|-------------------------------------------------------------------------|
|1. | Afficher le temps d’exécution du pipeline de build pour un build donné | L'affichage  du temps d'un build permet de faire des analyses sur le temps que l'equipe de devops peut deployer les changements apportes au projet. |
|2. | Afficher le temps d'execiton des tests automatises pour un run donné | L'affichage du temps d'execution des tests automatisee permet de faire des analyses sur le temps total d'un deployement |
|3. | Temps de tests moyen pour l'ensemble des job | La moyenne de temps d'exécution des tests automatisés pour l'ensemble des job (si applicable) |
|4. | Afficher la quantité de tests automatisés réussis et échoués | Comparer les tests reussies avec les tests echouees. Cela permet de voir s'il y a plus des tests echouees que reussi et savoir s'il faut prendre des mesures pour les reduire. Ainsi, les risques des pertes de temps et d'argent diminueront |


## Base de données
Le programme métriques n'a plus de menu console. Tout les métriques sont enregistrées dans la base de données à chaque exécution du code.

## Grafana
Tout le métriques sont affichés dans Grafana (http://35.203.86.116:3000/). Il faut se connecter à Grafana poir visualiser les métriques.
