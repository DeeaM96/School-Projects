import requests


def get_request(url):
    username = "DavidOlivierBouchard"
    token = "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"

    response = requests.get(url, auth=(username, token))
    return response