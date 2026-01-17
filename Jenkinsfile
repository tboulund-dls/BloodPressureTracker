pipeline{
    agent any
    triggers{
        pollSCM('* * * * *')
    }
    stages{
        stage('Checkout'){
            steps{
                checkout scm
            }
        }
        stage('Build'){
            steps{
                echo 'Building the application'
                sh 'docker --version'
                sh 'docker-compose build'
            }
        }
        stage('prepare test'){
            steps{
                echo 'prepare test'
                // sh 'docker-compose up -d bloodpressure'
                // sh 'docker-compose up -d patient'
                // sh 'docker-compose up -d mesurment'
                // sh 'docker-compose up -d featurehub'
                // sh 'docker-compose up -d migration_service'
                sh 'dotnet build Mesurment'
                sh 'dotnet build Patient'
                sh 'dotnet build FeatureHub'
            }
        }
        stage('Test'){
            steps{
                echo 'Testing the application'
                sh 'dotnet test'
            }
        }
        stage('Deploy'){
            steps{
                echo 'Deploying the application'
            }
        }
    }
    post {
        always {
            echo 'Cleaning up resources...'
            sh 'docker-compose down'
        }
    }
}