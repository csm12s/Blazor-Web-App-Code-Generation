dotnet build

cd src/ui/ng-alain
npm install && npm run-script build && del ..\..\TuTu.PaiKe.Web\wwwroot\* /q && copy dist\* ..\..\TuTu.PaiKe.Web\wwwroot\ && cd ..\..\TuTu.PaiKe.Web && dotnet build -c Release && dotnet publish -c Release && cd ..\..\ && docker build -t tutu.paike.web .
