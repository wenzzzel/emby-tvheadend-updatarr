<h1 align="center">
emby-tvheadend-updatarr
</h1>

<p align="center">
	<a href="https://github.com/wenzzzel/emby-tvheadend-updatarr/stargazers"><img src="https://img.shields.io/github/stars/wenzzzel/emby-tvheadend-updatarr?colorA=363a4f&colorB=b7bdf8&style=for-the-badge"></a>
	<a href="https://github.com/wenzzzel/emby-tvheadend-updatarr/issues"><img src="https://img.shields.io/github/issues/wenzzzel/emby-tvheadend-updatarr?colorA=363a4f&colorB=f5a97f&style=for-the-badge"></a>
	<a href="https://github.com/wenzzzel/emby-tvheadend-updatarr/contributors"><img src="https://img.shields.io/github/contributors/wenzzzel/emby-tvheadend-updatarr?colorA=363a4f&colorB=a6da95&style=for-the-badge"></a>
</p>
<p align="center">
    <img src="assets/logo.jpeg" style="width: 500px; height: auto; border-radius:10px"/>
</p>

## ℹ️ About
A simple .net application which enforces a refresh of the tvheadend plugin in emby. Reason for this solution is that the tvheadend plugin doesn't natively have refresh functionality.

## 🏃‍➡️ How to run
Mandatory environment variables
 - EMBY_TVHEADEND_UPDATARR_CRON
 - EMBY_API_KEY
 - EMBY_SERVER_BASE_URL

Optional environment variables
 - RUN_ONCE

## 🐋 Docker image
wenzzzel/emby-tvheadend-updatarr on [docker hub](https://hub.docker.com/repository/docker/wenzzzel/emby-tvheadend-updatarr/general)