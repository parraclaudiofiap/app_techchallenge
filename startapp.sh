#!/bin/bash

echo 'Subindo base dados...'

kubectl apply -f kubernetes/mongodb/mongodb-pv.yaml

kubectl apply -f kubernetes/mongodb/mongodb-pvc.yaml

kubectl apply -f kubernetes/mongodb/mongodb-deployment.yaml

kubectl apply -f kubernetes/mongodb/mongodb-svc.yaml

echo 'Construindo a imagem da aplicacao ...'

docker build -t techchallenge02 .


echo 'Realizando o deploy da aplicação...'

sleep 60

kubectl apply -f kubernetes/app/app-deployment.yaml

kubectl apply -f kubernetes/app/app-hpa.yaml

kubectl apply -f kubernetes/app/app-svc.yaml

https=http://localhost:30000/health/ready
status=0
while [ $status -eq 0 ]
do
  echo 'Checando saude da aplicacao'
  sleep 5
  status=`curl $https -k -s -f -o /dev/null && echo 1 || echo 0`
done

echo 'Ambiente pronto !'


if [[ "$OSTYPE" == "linux-gnu"* ]]; then
    xdg-open http://localhost:30000/docs
    xdg-open http://localhost:30000/swagger
elif [[ "$OSTYPE" == "darwin"* ]]; then
    open http://localhost:30000/docs
    open http://localhost:30000/swagger
else
    start http://localhost:30000/docs
    start http://localhost:30000/swagger
fi