FROM mcr.microsoft.com/dotnet/aspnet:5.0.9
ENV TZ=Asia/Shanghai
#System.Drawing.Common
RUN sed -i "s@http://deb.debian.org@http://mirrors.aliyun.com@g" /etc/apt/sources.list 
RUN apt-get update -y && apt-get install -y libgdiplus locales fontconfig && apt-get clean && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
WORKDIR /app
EXPOSE 80
COPY . .
ENTRYPOINT ["dotnet", "Gardener.Entry.dll"]
