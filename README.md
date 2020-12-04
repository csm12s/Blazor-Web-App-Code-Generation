<p align="center"><img src="https://images.gitee.com/uploads/images/2020/1204/145903_cea2bf9d_302533.png" height="80"/></p>

## 介绍

园丁是基于 .net 5开发的后台管理系统，系统前后台分离，api 是基于Furion 框架开发，前端是基于ant-design-blazor开发，系统使用技术或框架较新，适合学习使用。
## 已有功能
- 权限控制
  - 客户端登录验证
  - 客户端页面资源验证（展示信息、按钮）
  - 服务端api请求验证
- 用户管理
- 角色管理
- 资源管理

## 特点
- 新：.Net5 、Blazor WebAssembly 、Furion ；全部新鲜。
- 简：仅实现管理系统需要的功能，没有多余（懒。。）

## 开始使用
1. 确保安装了.net 5 sdk，如果使用vs,确保是vs2019最新版
2. 打开 API.sln 在Gardener.EntityFramwork.Core/dbsettings.json 中修改 GardenerMysqlDbConnectionString
3. 设置Gardener.Web.Entry 为启动项目，打开 工具=> Nuget包管理器=> 程序包管理器控制台 执行EF迁移命令`Add-Migration v0.0.1`，`Update-Database`,成功后 F5启动接口
4. 打开 Client.sln 设置 Gardener.Client 为启动项目，F5启动Client或右击wwwroot在浏览器打开
5. 默认用户名密码 admin/admin、testuser/testuser

## 展示

<img src="https://images.gitee.com/uploads/images/2020/1204/160750_e2d69ed2_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2020/1204/160758_7192619c_302533.png" width="260px"/>
<img src="https://images.gitee.com/uploads/images/2020/1204/160739_fe82dff5_302533.png" width="260px"/>

## 链接
👉 **[Furion](https://gitee.com/monksoul/Furion)**
👉 **[ant-design-blazor](https://github.com/ant-design-blazor/ant-design-blazor)**

