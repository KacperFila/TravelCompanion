FROM node:23.8.0 as build

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .

# Set defaults, can be overridden via docker-compose or docker run
ENV ENV=production
ENV API_URL=https://api.example.com

# Generate environment.ts file
RUN node ./scripts/set-env.js

# Build Angular app for the given environment
RUN npm install -g @angular/cli && ng build --configuration $ENV

# Use NGINX to serve built app
FROM nginx:alpine
COPY --from=build /app/dist/ui /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
