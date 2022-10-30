# GTI525 serveur back-end

## Project setup

```
npm install
```

### Compiles and hot-reloads for development

```
npm run start
```

# Examples

## Get daily data by stationId

```
# api call
localhost:3000/daily-data/51157?year=2020&month=01&day=10

# response format
[
    {
        "longitude": "-73.74",
        "latitude": "45.47",
        "stationName": "MONTREAL INTL A",
        "climateId": "7025251",
        "dateTime": "2020-01-01 00:00",
        "year": "2020",
        "month": "01",
        "day": "01",
        "time": "00:00",
        "temp": "0.0",
        "tempFlag": "",
        "dewPointTemp": "-2.0",
        "dewPointTempFlag": "",
        "relHum": "87",
        "relHumFlag": "",
        "precipAmount": "",
        "precipAmountFlag": "",
        "windDir": "24",
        "windDirFlag": "",
        "windsPd": "17",
        "windsPdFlag": "",
        "visibility": "19.3",
        "visibilityFlag": "",
        "stnPress": "99.29",
        "stnPressFlag": "",
        "hmdx": "",
        "hmdxFlag": "",
        "windChill": "-5",
        "windChillFlag": "",
        "weather": "NA"
    },
    ...
]
```

## Get previsions

```
# api call
localhost:3000/prevision/YYJ

# response format
{
    "feed": {
        "$": {
            "xmlns": "http://www.w3.org/2005/Atom",
            "xml:lang": "fr-ca"
        },
        "title": [
            "Victoria - MC)tC)o - Environnement Canada"
        ],
        "link": [
            {
                "$": {
                    "rel": "related",
                    "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html",
                    "type": "text/html"
                }
            },
            {
                "$": {
                    "rel": "self",
                    "href": "https://www.meteo.gc.ca/rss/city/bc-85_f.xml",
                    "type": "application/atom+xml"
                }
            },
            {
                "$": {
                    "rel": "alternate",
                    "hreflang": "en-ca",
                    "href": "https://www.weather.gc.ca/rss/city/bc-85_e.xml",
                    "type": "application/atom+xml"
                }
            }
        ],
        "author": [
            {
                "name": [
                    "Environnement Canada"
                ],
                "uri": [
                    "https://www.meteo.gc.ca"
                ]
            }
        ],
        "updated": [
            "2022-10-29T19:22:22Z"
        ],
        "id": [
            "tag:meteo.gc.ca,2013-04-16:20221029192222"
        ],
        "logo": [
            "https://www.meteo.gc.ca/template/gcweb/v5.0.1/assets/wmms-alt.png"
        ],
        "icon": [
            "https://www.meteo.gc.ca/template/gcweb/v5.0.1/assets/favicon.ico"
        ],
        "rights": [
            "Droit d'auteur 2022, Environnement Canada"
        ],
        "entry": [
            {
                "title": [
                    "BULLETIN MC\tTC\tOROLOGIQUE SPC\tCIAL , Victoria"
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/warnings/report_f.html?bc43"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T16:46:00Z"
                ],
                "published": [
                    "2022-10-29T16:46:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "Veilles et avertissements"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Le public de la rC)gion concernC)e doit porter une attention particuliC(re aux conditions mC)tC)orologiques potentiellement dangereuses et prendre les mesures de sC)curitC) qui s'imposent. C\tmise C : 09h46 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_w1:202210291646"
                ]
            },
            {
                "title": [
                    "Conditions actuelles: GC)nC)ralement nuageux, 9,9°C"
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T19:00:00Z"
                ],
                "published": [
                    "2022-10-29T19:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "Conditions actuelles"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "<b>EnregistrC)es C :</b> AC)roport int. de Victoria 12h00 HAP samedi 29 octobre 2022 <br/>\n<b>Condition:</b> GC)nC)ralement nuageux <br/>\n<b>TempC)rature:</b> 9,9&deg;C <br/>\n<b>Pression / Tendance:</b> 102,5 kPa C  la hausse<br/>\n<b>VisibilitC):</b> 48,3 km<br/>\n<b>HumiditC):</b> 87 %<br/>\n<b>Point de rosC)e:</b> 7,9&deg;C <br/>\n<b>Vent:</b> SE 28 km/h<br/>\n<b>Cote air santC):</b> 1 <br/>\n",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_cc:20221029190000"
                ]
            },
            {
                "title": [
                    "Samedi: PossibilitC) d'averses. Maximum 13. PdP 30%"
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Alternance de soleil et de nuages. 30 pour cent de probabilitC) d'averses cet aprC(s-midi. Vents du sud-est de 20 km/h avec rafales C  40. Maximum 13. Indice UV de 2 ou bas. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc1:20221029180000"
                ]
            },
            {
                "title": [
                    "Ce soir et cette nuit: PossibilitC) d'averses. Minimum 9. PdP 60%"
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Nuageux. 60 pour cent de probabilitC) d'averses ce soir. Pluie dC)butant vers minuit. Vents du sud-est de 20 km/h avec rafales C  40. Minimum 9. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc2:20221029180000"
                ]
            },
            {
                "title": [
                    "Dimanche: Pluie. Maximum 14."
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Pluie. Vents du sud-est de 20 km/h avec rafales C  40 devenant du sud-ouest C  30 avec rafales C  50 en aprC(s-midi. Maximum 14. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc3:20221029180000"
                ]
            },
            {
                "title": [
                    "Dimanche soir et nuit: Pluie. Minimum 9."
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Pluie. Minimum 9. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc4:20221029180000"
                ]
            },
            {
                "title": [
                    "Lundi: PossibilitC) d'averses. Maximum 11. PdP 40%"
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Nuageux avec 40 pour cent de probabilitC) d'averses. Venteux. Maximum 11. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc5:20221029180000"
                ]
            },
            {
                "title": [
                    "Lundi soir et nuit: PossibilitC) d'averses. Minimum 6. PdP 40%"
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Nuageux avec 40 pour cent de probabilitC) d'averses. Minimum 6. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc6:20221029180000"
                ]
            },
            {
                "title": [
                    "Mardi: PossibilitC) d'averses. Maximum 10. PdP 40%"
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Alternance de soleil et de nuages avec 40 pour cent de probabilitC) d'averses. Maximum 10. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc7:20221029180000"
                ]
            },
            {
                "title": [
                    "Mardi soir et nuit: Passages nuageux. Minimum 6."
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Passages nuageux. Minimum 6. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc8:20221029180000"
                ]
            },
            {
                "title": [
                    "Mercredi: EnsoleillC). Maximum 11."
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "EnsoleillC). Maximum 11. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc9:20221029180000"
                ]
            },
            {
                "title": [
                    "Mercredi soir et nuit: Nuageux. Minimum plus 3."
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Nuageux. Minimum plus 3. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc10:20221029180000"
                ]
            },
            {
                "title": [
                    "Jeudi: Pluie intermittente. Maximum 10."
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Pluie intermittente. Maximum 10. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc11:20221029180000"
                ]
            },
            {
                "title": [
                    "Jeudi soir et nuit: Pluie. Minimum 7."
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Pluie. Minimum 7. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc12:20221029180000"
                ]
            },
            {
                "title": [
                    "Vendredi: Pluie. Maximum 13."
                ],
                "link": [
                    {
                        "$": {
                            "type": "text/html",
                            "href": "https://www.meteo.gc.ca/city/pages/bc-85_metric_f.html"
                        }
                    }
                ],
                "updated": [
                    "2022-10-29T18:00:00Z"
                ],
                "published": [
                    "2022-10-29T18:00:00Z"
                ],
                "category": [
                    {
                        "$": {
                            "term": "PrC)visions mC)tC)o"
                        }
                    }
                ],
                "summary": [
                    {
                        "_": "Pluie. Maximum 13. PrC)visions C)mises 11h00 HAP samedi 29 octobre 2022",
                        "$": {
                            "type": "html"
                        }
                    }
                ],
                "id": [
                    "tag:meteo.gc.ca,2013-04-16:bc-85_fc13:20221029180000"
                ]
            }
        ]
    }
}
```
