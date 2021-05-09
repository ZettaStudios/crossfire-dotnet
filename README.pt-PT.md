![](banner.jpg)
[![](https://img.shields.io/discord/838558080621871114?label=Discord&logo=discord&style=flat-square)](https://discord.gg/M44QqJmw3u)
[![](https://img.shields.io/github/workflow/status/ZettaStudios/crossfire-dotnet/Windows%20-%20.NET%20Core?label=Windows%20Build&logo=windows&logoColor=%23FFFFFF&style=flat-square)](https://github.com/ZettaStudios/crossfire-dotnet/actions/workflows/windows-dotnet.yml)
[![](https://img.shields.io/github/workflow/status/ZettaStudios/crossfire-dotnet/Linux%20-%20.NET%20Core?label=Linux%20Build&logo=linux&logoColor=%23FFFFFF&style=flat-square)](https://github.com/ZettaStudios/crossfire-dotnet/actions/workflows/linux-dotnet.yml)
[![](https://img.shields.io/github/workflow/status/ZettaStudios/crossfire-dotnet/macOS%20-%20.NET%20Core?label=macOS%20Build&logo=apple&logoColor=%23FFFFFF&style=flat-square)](https://github.com/ZettaStudios/crossfire-dotnet/actions/workflows/macos-dotnet.yml)

[English](README.md) | Português

# CrossFire Emulator (Versão C#)
**Olá a todos!** Este é um **projecto ambicioso**, temos ainda muitos objetivos a atingir e gostaríamos de **apreciar** qualquer ajuda que nos é fornecido. Estamos felizes por disponibilizar o **código fonte** do nosso servidor, muitos dos Enum já criados estão na ordem correta e com a sua correta escrita para enviar os packets. Utilize o servidor como desejar, não temos restrições desde que atribua os créditos deste repositório.

## Visão geral
Isto é um projeto que está a ser desenvolvida pela comunidade, e não está afiliado a nenhuma das empresas pertencentes à [Neowiz](https://www.neowiz.com/), [Playgra (Arario)](http://playgra.com/), [VTC Game](https://www.vtcgame.vn/), [Tencent](https://www.tencent.com/), [Z8Games](https://www.z8games.com/), [GameClub](https://www.gameclub.ph/) and [LYTO](https://www.lytogame.com/). O projeto é desenvolvido com o .NET Core 3.1.

## Autenticação no servidor de Login
O projeto ainda não tem qualquer tipo de gestão de bases de dados ou simuladores, o mesmo para simular o servidor de login, utilizamos dados estáticos para efeitos de testes. Mais tarde, será implementada toda uma interface para este tipo de gestão.

Abaixo estão os dados para efetuar a autenticação.

| |LOGIN|PASSWORD|
|---|---|---|
|Conta|`oreki`|`oreki`

## UML Diagrams
Um pequeno esquema sobre a network do servidor.

![](diagram.png)
