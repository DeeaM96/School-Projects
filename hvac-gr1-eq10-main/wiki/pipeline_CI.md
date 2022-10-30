# Wiki - pipeline_CI

Le fichier [automation.yaml](https://github.com/log680-gr1-eq10/hvac-gr1-eq10/blob/main/.github/workflows/automation.yaml) est le pipeline CI
du projet HVAC. Il permet d'automatiser les tests unitaires et la construction de l'image sur DockerHub.


## Fichier YAML
Le fichier est composé de deux jobs : 
1. unit_test
2. build_and_push_to_Docker

### job unit_test
Cette job permet d'exécuter les tests unitaires à l'aide de la commande « pytest ». Les tests se situent dans le fichier
[test_main.py](https://github.com/log680-gr1-eq10/hvac-gr1-eq10/blob/main/test/test_main.py).

*Étapes d'exécution de la job :*
1. Configuration de l'environnement pour fonctionner avec python 3.10
2. Installation des dépendances et des conditions (requirements)
`python -m pip install --upgrade pip` `pip install pytest` `pip install -r requirements.txt`
3. Exécution des tests

Cette job est effectué à chaque push ou pull_request, sur *toutes* les branches

### job build_and_push_to_Docker
Permet de construire, tagger et envoyer (push) l'image sur DockerHub.
Cette job est exécutée uniquement lors d'un push sur le branche `main` et lorsque les tests unitaires ont tous passés

*Étapes d'exécution de la job :*
1. Récupération de la date sous format %Y%m%d%H%M
2. Connexion au compte DockerHub (variables enregistrés sur GitHub `secrets.DOCKERHUB_USERNAME` et `secrets.DOCKERHUB_TOKEN`)
3. Construction de l'image et envoie de l'image.

La construction et l'envoie de l'image se fait avec les tags `davidolivierbouchardets/hvac-container:${{ steps.date.outputs.date }}` et
`davidolivierbouchardets/hvac-container:latest`

## Intégration pipeline CD
```
deploy_cd:
    name: deploy_cd_pipeline
    needs: [unit_test, build_and_push_to_Docker]
    runs-on: ubuntu-latest
    if: ${{ (github.ref == 'refs/heads/main') && (github.event_name == 'push') && (success()) }}
    steps:
      - uses: actions/checkout@v3

      - name: delete old deployment
        uses: actions-hub/kubectl@master
        env:
          KUBE_CONFIG: ${{ secrets.KUBE_CONFIG }}
        with:
          args: delete deployment hvac-deployment


      - name: create and apply new deployment
        uses: actions-hub/kubectl@master
        env:
          KUBE_CONFIG: ${{ secrets.KUBE_CONFIG }}
        with:
          args: apply -f ./.github/kube-config.yaml
```

- L'image est poussée sur Kubernetes seulement si les 2 premières job sont écxécutées avec succès
- KUBE_CONFIG est un secret stocker dans les paramètres du repo Github (secrets)


## Commandes utiles
### Récupération et exécution manuel d'un container

```
> docker login
> docker pull davidolivierbouchardets/hvac-container:<tag>
> docker run -e HVAC_TOKEN=<token> davidolivierbouchardets/hvac-container:<tag>
```

### Construction et push manuel sur Docker
```
> docker build --tag davidolivierbouchardets/hvac-container:<tag> .
> docker push davidolivierbouchardets/hvac-container:<tag>
> docker run davidolivierbouchardets/hvac-container:<tag>
```

