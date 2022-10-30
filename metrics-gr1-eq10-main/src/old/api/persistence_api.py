import requests


username = "DavidOlivierBouchard"
token = "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"
url = "https://api.github.com"

def get(url):
    res = requests.get(url, auth=(username, token))
    return res.json()


def get_columns(project_id):
    res = requests.get(f"{url}/projects/{project_id}/columns", auth=(username, token))
    return res.json()

def get_cards(column_id):
    res = requests.get(f"{url}/projects/columns/{column_id}/cards", auth=(username, token))
    return res.json()

