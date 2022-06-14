# Build Image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /build

COPY . .
RUN dotnet restore "SkillsApi.csproj"
RUN dotnet publish -c Release -o /app

# Copy output of build to runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SkillsApi.dll"]

# Build Image
# docker build --rm -f Dockerfile -t skillsapi:config .

# Run Image
# docker run -d --rm -p 5051:80 skillsapi:config

# Browse using: 
# http://localhost:5051/api/skills

# Publish Image to dockerhub
# docker tag skillsapi:config arambazamba/skillsapi:config
# docker push arambazamba/skillsapi:config