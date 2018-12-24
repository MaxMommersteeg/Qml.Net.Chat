@echo off
title Starting Chat.Server
call cd src
call cd Qml.Net.Chat.Server
call dotnet restore
call dotnet run
call cls