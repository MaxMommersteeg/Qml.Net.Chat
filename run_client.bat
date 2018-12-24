@echo off
title Starting Chat.Client
call cd src
call cd Qml.Net.Chat.Client
call dotnet restore
call dotnet run
call cls