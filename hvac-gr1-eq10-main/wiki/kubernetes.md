# Wiki - Kubernetes et commandes kubectl
## Description
Wiki pour comprendre la configuration et les différentes commandes utilisées pour déployer l'image Docker sur Kubernetes

## Informatin

### Fichier de déploiement
Emplacement du fichier YAML contant les configurations de déploiement : `.github/kube-config.yaml`

## Fichier YAML
Le fichier YAML contient trois (3) sections :
- Configuration du déploiement (Deployment)
- Configuration des variables d'environnements avec Config Map
- Configuration des secrets

### Deployment
Configuration du deploiement de l'image Docker sur Kubernetes
```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: hvac-deployment
  namespace: team01-10
  labels:
    app: hvac
spec:
  selector:
    matchLabels:
      app: hvac
  template:
    metadata:
      labels:
        app: hvac
    spec:
      containers:
        - name: hvac
          image: davidolivierbouchardets/hvac-container:latest
          resources:
            requests:
              memory: "64Mi"
              cpu: "75m"
            limits:
              memory: "64Mi"
              cpu: "150m"
          ports:
            - containerPort: 80
          envFrom:
            - secretRef:
                name: hvac-token
            - configMapRef:
                name: hvac-env
      imagePullSecrets:
        - name: dockercred
```


### Config Map
Pour configuration des variables d'environnement du programme hvac

```
apiVersion: v1
kind: ConfigMap
metadata:
  name: hvac-env
data:
  HVAC_HOST: "http://35.203.86.116"
  COLD_TH: "12"
  HOT_TH: "30"
  NB_TICKS: "8"
```

### Secret

**Secret pour le token de connexion**
```
apiVersion: v1
kind: Secret
metadata:
  name: hvac-token
  namespace: team01-10
data:
  HVAC_TOKEN: <base64 encoded token>
type: Opaque
```

**Secret pour les credentials Docker**
```
apiVersion: v1
kind: Secret
metadata:
  name: dockercred
  namespace: team01-10
data:
  .dockerconfigjson: <Encoded creds>
type: kubernetes.io/dockerconfigjson
```

Dans le deployment  :
```
apiVersion: apps/v1
kind: Deployment
[...]
    spec:
      containers:
          [...]
          envFrom:
            - secretRef:
                name: hvac-token
            - configMapRef:
                name: hvac-env
```

## Secret

### Création manuel du secret des credentials Docker
Dans le command prompt ou PowerShell :
```
> docker login
> kubectl create secret docker-registry dockercred --docker-server=https://index.docker.io/v1/ --docker-username=davidolivierbouchardets --docker-password=SECRET --docker-email=david-olivier.bouchard.1@ens.etsmtl.ca
```
- Une nouvelle clé `dockercred` sera créé et pourra être utilisé dans le fichier de déploiement
- SECRET = Mot de passe Dockerhub

### Création manuel du secret pour les variables d'environnements
Créer un secret pour configurer des variables d'environnements :
```
> kubectl create secret generic hvac-token --from-literal=HVAC_TOKEN=SECRET
```
- SECRET : token HVAC_TOKEN


## Commandes kubectl
*Toutess les commandes suivantes ont été exécutées sur PowerShell à la racine du projet hvac*
  
  - Déployer sur Kubernetes : `> kubectl apply -f .\.github\deployment.yaml`
  - Voir les déploiements : `> kubectl get deployments`
  - Voir les services (ressources) : `> kubectl get services`
  - Afficher les logs de déploiement : `> kubectl logs deployment/hvac-deployment`
  - Voir le documents de secrets (pour les variables d'environnements et identification Docker) : `> kubectl edit secrets`
  - Voir le doc de config map : `> kubectl edit configmap`
