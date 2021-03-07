pipeline{
    agent any
    
    environment {
        dotnet ='C:\\Program Files (x86)\\dotnet\\'
    }

    stage('Checkout') {
        steps {
            git 
                credentialsId: '9898dd46-3ab4-45e1-87b2-96703c808edb', 
                url: 'https://github.com/sagar-agola/OnlineParking.git/', 
                branch: 'master'
        }
    }

    stage('Restore packages'){
        steps{
            bat "dotnet restore PBS.Api\\PBS.Api.csproj"
        }
    }

    stage('Clean'){
        steps{
            bat "dotnet clean PBS.Api\\PBS.Api.csproj"
        }
    }

    stage('Build'){
        steps{
            bat "dotnet build PBS.Api\\PBS.Api.csproj --configuration Release"
        }
    }

    stage('Publish'){
        steps{
        bat "dotnet publish PBS.Api\\PBS.Api.csproj"
        }
    }
}