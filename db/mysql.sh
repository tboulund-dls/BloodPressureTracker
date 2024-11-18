docker exec -it bms_db mysql -uroot -pmy-secret-pw






docker build -t bms_db .
docker run --name bms_db  -p 3306:3306 bms_db
docker stop $(docker ps -aq)
docker rm $(docker ps -aq)
docker rmi $(docker images -q)
