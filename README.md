# FastDFSSample
FastDFS C# DevOps Sample with docker

**代码和配置中只需要按需修改IP、端口及挂载目录即可**

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

> https://www.nuget.org/packages/FastDFSNetCore/
> 
> https://github.com/caozhiyuan/FastDFSNetCore

> https://hub.docker.com/r/season/fastdfs
