# Load Docker Image from Backup tar

To restore the Docker container from a backup, first make sure that the backup image is present in the host machine.
If not, load the backup images using ‘docker load‘ command. Confirm that the image is present in the server using ‘docker images‘ command.

```bash
docker load -i backup-mongodb-before-mojave.tar
```

Once the backup images are listed in the Docker host, you can restore the container by using ‘docker run’ command and specifying the backup image.

```bash
docker run -d -p 27017:27017 --name mongodbKinderkultur backup-mongodb-before-mojave
```

```bash
docker run -d -p 3306:3306 --name mariadbKinderkultur backup-mariadb-before-mojave
```