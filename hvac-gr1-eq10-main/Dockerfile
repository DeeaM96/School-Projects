# syntax=docker/dockerfile:1

FROM python:3.7.2-alpine3.8

COPY requirements.txt requirements.txt

RUN pip3 install --no-cache-dir -r requirements.txt

COPY . .

CMD [ "python3", "-u", "src/main.py"]
