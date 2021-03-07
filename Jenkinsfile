pipeline{
    agent any
    
    environment {
        dotnet ='C:\\Program Files (x86)\\dotnet\\'
    }

    tages{
        stage('Checkout') {
            steps {
                git url: 'https://github.com/sagar-agola/OnlineParking.git/', 
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
}