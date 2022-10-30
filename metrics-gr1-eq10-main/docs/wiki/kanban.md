# Wiki - Kanban
## Description
Ce répertoire fonctionne avec un projet et un tableau Kanban. Ce tableau permet de voir l'avancement du projet et d'avoir une vue d'ensemble sur ce qui est fait et à faire.
Il est primordiale d'utiliser ce tableau lors d'implémentation de code dans ce répertoire.


## Colonnes
Le tableau Kanban comporte cinq (5) colonnes. Chaque colonne correspond à une étape pour une tâche ou un « pull request ».

| Colonne                                                    | Description                                                           |
|----------------------------------------------------------------|-------------------------------------------------------------------------|
| Backlog  | Toute les tâches à effectuer pour ce projet débute dans cette colonne |
| To Do (6 MAX) | Lorsqu'une tâche est assignée, il faut la déplacer dans cette colonne. Signifie que la tâche est à faire par cette personne, mais n'est pas encore débutée. 6 tâches MAX peuvent être dans cette colonne |
| In Progress (4 MAX) | Tâche qui est activement en train d'être développée. 4 tâches MAX peuvent être dans cette colonne |
| To be Reviewed (3 MAX) | Lorsqu'une tâche doit être passée en revue par un paire avant d'être déployée. Les « pull request » se retrouvent automatiquement dans cette colonne. 3 tâches MAX dand cette colonne|
| Done | Regroupement de toutes les tâches terminées |

## Utilisation du tableau Kanban
Chaque carte du tableau Kanban doit être représenté par une tâche (« issue ») ou un « pull request ». Les tâches sont automatiquement ajoutées à la colonne `Backlog`
lorsqu'elles sont créées. Les « pull request » sont automatiquement ajoutés à la colonne `To be Reviewed (3 MAX)`. Les tâches/« pull request » sont associés au tableau en 
sélectionnant le projet lors de leurs créations.

Lorsque une carte doit être déplacée, il suffit simplement de cliquer sur cette carte et de la déplacer vers la colonne désirée.

### Automatisation de classement
Certaines colonnes ont des automatisations d'activés. Ces automatisations permettent le déplacement automatique d'une carte vers une colonne lorsque le statut de cette
carte change. Voici un résumé de l'automatisation :
* Les nouvelles tâches sont automatiquement ajoutées à la colonne `Backlog`
* Les « pull request » sont automatiquement ajoutées à la colonne `To be Reviewed (3 MAX)`
* Les tâches et colonnes fermées sont automatiquement ajoutées à la colonne `Done`
