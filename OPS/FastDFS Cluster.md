# 环境

| 类型    | ip         | 端口  | 备注          |
| ------- | ---------- | ----- | ------------- |
| tracker | 10.4.52.14 | 22122 |               |
| tracker | 10.4.52.17 | 22122 |               |
| storage | 10.4.52.14 | 23000 | group1        |
| storage | 10.4.52.17 | 23000 | group1        |
| storage | 10.4.52.14 | 23001 | group2        |
| storage | 10.4.52.17 | 23001 | group2        |
| nginx   | 10.4.52.14 | 7003  | group1,group2 |
| nginx   | 10.4.52.17 | 7003  | group1,group2 |



# 初始化

## 10.4.52.14

```sh
mkdir -p /home/fastdfs
cd /home/fastdfs
mkdir -p nginx tracker_data group1/storage_data group1/store_path
mkdir -p group2/storage_data group2/store_path

docker pull season/fastdfs:1.2
```

## 10.4.52.17

```sh
mkdir -p /home/fastdfs
cd /home/fastdfs
mkdir -p nginx tracker_data group1/storage_data group1/store_path
mkdir -p group2/storage_data group2/store_path

docker pull season/fastdfs:1.2
```

# tracker

## 10.4.52.14

```
vi ./tracker.conf

# 见 10.4.52.14/tracker.conf
```

```
docker run -d --name fastdfs_tracker \
--net=host \
--restart=always \
--privileged=true \
-v /home/fastdfs/tracker_data:/fastdfs/tracker/data \
-v /home/fastdfs/tracker.conf:/fdfs_conf/tracker.conf \
season/fastdfs:1.2 \
tracker
```

```
docker logs fastdfs_tracker
docker restart fastdfs_tracker
truncate -s 0 /home/docker/containers/*/*-json.log
docker rm -fv fastdfs_tracker
docker exec -it fastdfs_tracker cat /etc/fdfs/tracker.conf
```

## 10.4.52.17

```
vi ./tracker.conf

# 见 10.4.52.17/tracker.conf
```

```
docker run -d --name fastdfs_tracker \
--net=host \
--restart=always \
--privileged=true \
-v /home/fastdfs/tracker_data:/fastdfs/tracker/data \
-v /home/fastdfs/tracker.conf:/fdfs_conf/tracker.conf \
season/fastdfs:1.2 \
tracker
```

```
docker logs fastdfs_tracker
docker restart fastdfs_tracker
truncate -s 0 /home/docker/containers/*/*-json.log
docker rm -fv fastdfs_tracker
docker exec -it fastdfs_tracker cat /etc/fdfs/tracker.conf
```

# storage group1

## 10.4.52.14

```
vi ./group1/storage.conf

# 见 10.4.52.14/group1/storage.conf
```

```
docker run -d --name fastdfs_storage_group1 \
--net host \
--restart=always \
--privileged=true \
-v /home/fastdfs/group1/storage_data:/fastdfs/storage/data \
-v /home/fastdfs/group1/store_path:/fastdfs/store_path \
-v /home/fastdfs/group1/storage.conf:/fdfs_conf/storage.conf \
season/fastdfs:1.2 \
storage
```

```
docker logs fastdfs_storage_group1
docker restart fastdfs_storage_group1
docker exec fastdfs_storage_group1 fdfs_monitor /etc/fdfs/storage.conf
truncate -s 0 /home/docker/containers/*/*-json.log
docker rm -fv fastdfs_storage_group1
```

## 10.4.52.17

```
vi ./group1/storage.conf

# 见 10.4.52.17/group1/storage.conf
```

```
docker run -d --name fastdfs_storage_group1 \
--net host \
--restart=always \
--privileged=true \
-v /home/fastdfs/group1/storage_data:/fastdfs/storage/data \
-v /home/fastdfs/group1/store_path:/fastdfs/store_path \
-v /home/fastdfs/group1/storage.conf:/fdfs_conf/storage.conf \
season/fastdfs:1.2 \
storage
```

```
docker logs fastdfs_storage_group1
docker restart fastdfs_storage_group1
docker exec fastdfs_storage_group1 fdfs_monitor /etc/fdfs/storage.conf
truncate -s 0 /home/docker/containers/*/*-json.log
docker rm -fv fastdfs_storage_group1
```

# storage group2

## 10.4.52.14

```
vi ./group2/storage.conf

# 见 10.4.52.14/group2/storage.conf
```

```
docker run -d --name fastdfs_storage_group2 \
--net host \
--restart=always \
--privileged=true \
-v /home/fastdfs/group2/storage_data:/fastdfs/storage/data \
-v /home/fastdfs/group2/store_path:/fastdfs/store_path \
-v /home/fastdfs/group2/storage.conf:/fdfs_conf/storage.conf \
season/fastdfs:1.2 \
storage
```

```
docker logs fastdfs_storage_group2
docker restart fastdfs_storage_group2
docker exec fastdfs_storage_group2 fdfs_monitor /etc/fdfs/storage.conf
truncate -s 0 /home/docker/containers/*/*-json.log
docker rm -fv fastdfs_storage_group2
```

## 10.4.52.17

```
vi ./group2/storage.conf

# 见 10.4.52.17/group2/storage.conf
```

```
docker run -d --name fastdfs_storage_group2 \
--net host \
--restart=always \
--privileged=true \
-v /home/fastdfs/group2/storage_data:/fastdfs/storage/data \
-v /home/fastdfs/group2/store_path:/fastdfs/store_path \
-v /home/fastdfs/group2/storage.conf:/fdfs_conf/storage.conf \
season/fastdfs:1.2 \
storage
```

```
docker logs fastdfs_storage_group2
docker restart fastdfs_storage_group2
docker exec fastdfs_storage_group2 fdfs_monitor /etc/fdfs/storage.conf
truncate -s 0 /home/docker/containers/*/*-json.log
docker rm -fv fastdfs_storage_group2
```

# nginx

## 10.4.52.14

```
vi ./nginx/nginx.conf

# 见 10.4.52.14/nginx/nginx.conf
```

```
vi ./nginx/mod_fastdfs.conf

# 见 10.4.52.14/nginx/mod_fastdfs.conf
```

```
docker run -d --name fastdfs_nginx \
--net host \
--restart=always \
--privileged=true \
-v /home/fastdfs/group1/store_path:/fastdfs/store_path/group1 \
-v /home/fastdfs/group2/store_path:/fastdfs/store_path/group2 \
-v /home/fastdfs/nginx/nginx.conf:/etc/nginx/conf/nginx.conf \
-v /home/fastdfs/nginx/mod_fastdfs.conf:/fdfs_conf/mod_fastdfs.conf \
season/fastdfs:1.2 \
nginx
```

```
docker logs fastdfs_nginx
docker restart fastdfs_nginx
docker exec -it fastdfs_nginx bash
truncate -s 0 /home/docker/containers/*/*-json.log
docker rm -fv fastdfs_nginx
```

## 10.4.52.17

```
vi ./nginx/nginx.conf

# 见 10.4.52.17/nginx/nginx.conf
```

```
vi ./nginx/mod_fastdfs.conf

# 见 10.4.52.17/nginx/mod_fastdfs.conf
```

```
docker run -d --name fastdfs_nginx \
--net host \
--restart=always \
--privileged=true \
-v /home/fastdfs/group1/store_path:/fastdfs/store_path/group1 \
-v /home/fastdfs/group2/store_path:/fastdfs/store_path/group2 \
-v /home/fastdfs/nginx/nginx.conf:/etc/nginx/conf/nginx.conf \
-v /home/fastdfs/nginx/mod_fastdfs.conf:/fdfs_conf/mod_fastdfs.conf \
season/fastdfs:1.2 \
nginx
```

```
docker logs fastdfs_nginx
docker restart fastdfs_nginx
truncate -s 0 /home/docker/containers/*/*-json.log
docker rm -fv fastdfs_nginx
```





