# NGINX

[NGINX](https://www.nginx.com/)

[NGINX Documentation](https://docs.nginx.com/)

## Demo

Build Image and run Container:

```bash
docker build --rm -f "dockerfile" -t nginxapp .
docker run -d --rm -p 5050:80 nginxapp
```

Browse using `http://localhost:5050/`