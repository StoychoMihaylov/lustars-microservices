
ARG NODE_VERSION=13.6.0
ARG ALPINE_VERSION=3.11

FROM node:${NODE_VERSION}-alpine${ALPINE_VERSION}
LABEL maintainer="Lustars dev team"

ENV workdir /app

WORKDIR ${workdir}

ENV PATH ${workdir}/node_modules/.bin:$PATH

COPY package.json ${workdir}/package.json
RUN npm install --silent
RUN npm install react-scripts -g --silent

COPY . .

CMD ["npm", "start"]

