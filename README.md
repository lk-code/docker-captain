# DockerCaptain

![DockerCaptain](https://raw.githubusercontent.com/lk-code/docker-captain/main/icon_128.png)

[![.NET Version](https://img.shields.io/badge/dotnet%20version-net6.0-blue?style=flat-square)](http://www.nuget.org/packages/DockerCaptain/)
[![License](https://img.shields.io/github/license/lk-code/docker-captain.svg?style=flat-square)](https://github.com/lk-code/docker-captain/blob/master/LICENSE)
[![Build](https://github.com/lk-code/docker-captain/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/lk-code/docker-captain/actions/workflows/dotnet-desktop.yml)
[![Downloads](https://img.shields.io/nuget/dt/dockercaptain.svg?style=flat-square)](http://www.nuget.org/packages/dockercaptain/)
[![NuGet](https://img.shields.io/nuget/v/dockercaptain.svg?style=flat-square)](http://nuget.org/packages/dockercaptain)

[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=lk-code_docker-captain&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=lk-code_docker-captain)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=lk-code_docker-captain&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=lk-code_docker-captain)

This tool is a cross-platform helper (currently for macOS, Ubuntu and Windows - more operating systems are planned) and brings various supports for Docker.

## install the tool

`dotnet tool install --global DockerCaptain`

## update the tool

`dotnet tool update --global DockerCaptain`

## user config

edit the file config.json in the app directory (you can find the path via `captain info`)

possible config values:

* docker (string) - "/path/to/docker/executable.exe"
* appdirectory (string) - "/path/to/app/directory" (notice: config.json must ALWAYS be located at the original app-directory!)

## usage

### display info

displays the app version and the path to app-directory and docker-binaries.

`captain info`

### register a image

register the given docker image and pulls in docker the latest version

`captain images register {DOCKER_IMAGE_NAME} [-f][--force]`

* `-f` `--force` (optional)

**example**:

`captain images register lkcode/a-test-image`
