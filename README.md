<p align="center"><img src="https://images.gitee.com/uploads/images/2020/1204/145903_cea2bf9d_302533.png" height="80"/></p>

## 项目介绍

园丁是基于 .net 5开发的后台管理系统，系统前后台分离，api 是基于Furion 框架开发，前端是基于ant-design-blazor开发，系统使用技术或框架较新，适合学习使用。
## 已有功能
- 权限控制
  - 客户端登录验证
  - 客户端页面资源验证（展示信息、按钮）
  - 服务端api请求验证
- 用户管理
  - 角色分配、头像上传
- 角色管理
  - 资源分配
- 资源管理
- 附件管理
- 操作审计
- 数据审计

## 项目特点
- 新：.Net5 、Blazor WebAssembly 、Furion ；全部新鲜。
- 简：仅实现管理系统需要的功能，没有多余（懒。。）

## 开始使用
1. 确保安装了.net 5 sdk，如果使用vs,确保是vs2019最新版
2. 打开 API.sln 设置Gardener.Web.Entry 为启动项目，F5启动接口
3. 打开 Client.sln 设置 Gardener.Client 为启动项目，F5启动Client或右击wwwroot在浏览器打开（F5调试启动，较卡！！）
4. 默认用户名密码 admin/admin、testuser/testuser

## 项目结构

```
+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
+公共层	+---Gardener.Enums              	--公共枚举层                             
+       +---Gardener.Common             	--公共扩展层                             
+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
+       +---Gardener.Client.Models      	--Client实体模型层（client独有）         
+Client	+---Gardener.Client.Services    	--Client业务服务层                      
+       +---Gardener.Client             	--Client页面层                          
+       +---Gardener.ClientHost         	--Client宿主                            
+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
+       +---Gardener.Application        	--API端业务层（业务实现）              
+       +---Gardener.Application.Dtos   	--API端业务数据模型层（提供给Client） 
+       +---Gardener.Application.Interfaces     --API端业务接口定义层（提供给Client）  
+       +---Gardener.Core               	--API项目核心层（项目架构）             
+ API	+---Gardener.Core.Entites       	--API项目ORM实体模型                    
+       +---Gardener.Database.Migrations	--API数据库迁移（EF迁移）               
+       +---Gardener.EntityFramwork.Core	--API项目ORM EF核心层                  
+       +---Gardener.Web.Core           	--API Web核心层（Web框架核心及配置）    
+       +---Gardener.Web.Entry          	--API Web 服务入口                      
++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
```


## 常见问题
- 如何切换数据库：项目ORM框架使用的是EF，默认已使用sqlite，切换其它数据库首先需要通过nuget安装ef对应的支持包到`Gardener.EntityFramwork.Core`（[看这里](https://monksoul.gitee.io/furion/docs/dbcontext-multi-database)），然后需要调整以下文件
`Gardener.EntityFramwork.Core.DbContexts.GardenerDbContext`
`Gardener.EntityFramwork.Core/dbsettings.json`
`Gardener.EntityFramwork.Core/GardenerEntityFrameworkCoreStartup`
调整后开始迁移，设置Gardener.Web.Entry 为启动项目，打开 工具=> Nuget包管理器=> 程序包管理器控制台，控制台默认项目选 Gardener.Database.Migrations， 执行EF迁移命令`Add-Migration v0.0.1`，`Update-Database`即可。
- client 打不开：client默认端口是 44388，在 `Gardener.Client/launchSettings.json`中可以调整，浏览器应打开 https://localhost:44388
- 开发页面时如何热更新：在Gardener.Client目录执行`dotnet watch run`

## 界面展示

<img src="https://images.gitee.com/uploads/images/2020/1204/160750_e2d69ed2_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2020/1204/160758_7192619c_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2020/1204/160739_fe82dff5_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2021/0110/204258_f869ad45_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2021/0110/204309_f9c711ba_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2021/0110/204318_0cb9fadc_302533.png" width="260px"/>

## 基情链接
👉 **[Furion](https://gitee.com/monksoul/Furion)**
👉 **[ant-design-blazor](https://github.com/ant-design-blazor/ant-design-blazor)**

## 跟上组织
<a target="_blank" href="https://qm.qq.com/cgi-bin/qm/qr?k=ILV3MBrcZtr4uUSsKa3njjnpBiUvT0xe&jump_from=webapi"><img border="0" src="//pub.idqqimg.com/wpa/images/group.png" alt="学习交流群" title="学习交流群"></a>
