from datetime import datetime
from old.api import persistence_api
from tabulate import tabulate
import json

directory = "D:\Python Projects\LOG680 - Metric Project\Database\db.json"


def syncDatabase(project_id):
    print("Synchronisation en cours ...")

    now = datetime.now().strftime("%#d/%#m/%Y/%H:%M")

    year = now.split('/')[2]
    month = now.split('/')[1]
    day = now.split('/')[0]
    hour = now.split('/')[3]

    data = "{"
    data += '"year":' + str(year) + ','
    data += '"month":' + str(month) + ','
    data += '"day":' + str(day) + ','
    data += '"hour":' + "\"" + hour + "\","

    data += '"columns":['
    columns = persistence_api.get_columns(project_id)
    i = 0
    for column in columns:
        i += 1
        data += '{'
        name = column['name']
        column_id = column['id']
        data += '"name":' + "\"" + name + "\","
        data += '"id":' + "\"" + str(column_id) + "\","
        data += '"tasks":['

        tasks = persistence_api.get_cards(column_id)
        j = 0
        for task in tasks:
            j += 1
            data += '{'
            task_id = task['id']
            task_detail = persistence_api.get(task['content_url'])
            title = task_detail['title']
            creation_date = task_detail['created_at']
            data += '"id":' + str(task_id) + ','
            data += '"title":' + "\"" + title + "\","
            data += '"created_at":' + "\"" + creation_date + "\""
            data += '}'

            if j < len(tasks):
                data += ','

        data += "]}"
        if i < len(columns):
            data += ','

    data += ']}'
    data_json = json.loads(data)

    with open(directory) as fp:
        file = json.load(fp)

    file.append(data_json)

    with open(directory, 'w') as json_file:
        json.dump(file, json_file, indent=4, separators=(",", ":"))

    print("Synchronisation complétée ! ")


def menuMetric(project_id):
    print(""" 
        Fonctionnement : 

        Vous devez d'abord choisir une plage d'observation du projet. Ensuite, choisir une plage de création des
        tâches, c'est-à-dire sélectionner la plage où les tâches ont été créées.

        Étapes :

        1. Choisir un début de plage d'observation (date) - format M/D (Ex.: '2/3' (3 février))
        2. Choisir une fin de plage d'observation (date) - format M/D (Ex.: '5/23' (23 mai))
        3. Choisir un début de plage pour création de tâche (date) - format M/D (Ex.: '12/1' (1 décembre))
        4. Choisir une fin de plage pour création de tâche (date) - format M/D (Ex.: '4/5' (5 avril))
        """)

    start_obsv = input("DÉBUT de la plage d'obersvation (Format M/D) : ")
    end_obsv = input("FIN de la plage d'obersvation (Format M/D) : ")
    start_creation = input("DÉBUT de la plage de création de tâches (Format M/D) : ")
    end_creation = input("FIN de la plage de création de tâches (Format M/D) : ")

    filterColumns(start_obsv, end_obsv, start_creation, end_creation)


def filterColumns(start_obsv, end_obsv, start_creation, end_creation):
    start_obsv_month = int(start_obsv.split('/')[0])
    end_obsv_month = int(end_obsv.split('/')[0])
    start_obsv_day = int(start_obsv.split('/')[1])
    end_obsv_day = int(end_obsv.split('/')[1])
    start_creation_month = int(start_creation.split('/')[0])
    end_creation_month = int(end_creation.split('/')[0])
    start_creation_day = int(start_creation.split('/')[1])
    end_creation_day = int(end_creation.split('/')[1])

    start_obsv_date = datetime(2022, start_obsv_month, start_obsv_day)
    end_obsv_date = datetime(2022, end_obsv_month, end_obsv_day)
    start_creation_date = datetime(2022, start_creation_month, start_creation_day)
    end_creation_date = datetime(2022, end_creation_month, end_creation_day)

    printHeader(start_obsv, end_obsv, start_creation, end_creation)

    with open(directory) as json_file:
        file = json.load(json_file)

    columns_name_buffer = []
    results = {}
    for snapshot in file:
        month = snapshot["month"]
        day = snapshot["day"]
        snapshot_date = datetime(2022, month, day)
        if start_obsv_date <= snapshot_date <= end_obsv_date:
            for column in snapshot["columns"]:
                column_name = column["name"]
                if column_name not in columns_name_buffer:
                    columns_name_buffer.append(column_name)
                    results[f"{column_name}"] = []
                for task in column["tasks"]:
                    task_date = task["created_at"].split('T')[0].split("-")
                    task_month = int(task_date[1])
                    task_day = int(task_date[2])
                    task_date = datetime(2022, task_month, task_day)
                    if start_creation_date <= task_date <= end_creation_date:
                        if len(results[column_name]) > 0:
                            if task not in results[column_name]:
                                results[column_name].append(task)
                        else:
                            results[column_name].append(task)

    printResult(results)


def printHeader(start_obsv, end_obsv, start_creation, end_creation):
    print(f""" 
    Plage d'observation : du {start_obsv} au {end_obsv}
    Plage des tâches créées : du {start_creation} au {end_creation}
            
    Colonnes :
    ---------------------------------------------------------------
    """)


def printResult(results):
    for column_name in results.keys():
        tasks = []
        print(f"                                                    COLONNE :  {column_name}")

        for task in results[column_name]:
            tasks.append([task["title"], task["created_at"]])

        print(tabulate(tasks, headers=["Title", "Creation Date"], tablefmt="psql"))
        print("")
