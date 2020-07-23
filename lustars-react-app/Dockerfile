FROM node:latest
LABEL maintainer="Lustars dev team"

WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH

COPY package.json ./
COPY package-lock.json ./
RUN npm install --silent
RUN npm install react-scripts -g --silent

COPY . .

CMD ["npm", "start"]

