pipeline {
  agent any
  stages {
    stage('Build Test') {
        steps {
        sh '''
          # Update package list and install necessary dependencies
          apt-get update && \
          apt-get install -y \
          build-essential \
          cmake \
          g++ \
          libmysqlcppconn-dev \
          libpthread-stubs0-dev \
          libboost-all-dev \
          libstdc++6 \
          clang \
          && rm -rf /var/lib/apt/lists/*

          # Make sure the build.sh script is executable
          chmod +x src/build.sh
          chmod +x src/build_test.sh
          
          # Run the build script
          ./src/build_test.sh
        '''
        }
      }
    stage('Run Test') {
      steps {
        sh '''
        cd build_test
        ./BloodPressureTracker
        '''
      }
    }

    stage('Build Production') {
      steps {
        sh '''
        ./src/build.sh
        '''
      }
    }

    stage('Build Artifacts') {
      steps {
        archiveArtifacts artifacts: 'build/*BloodPressureTracker*', allowEmptyArchive: true
        archiveArtifacts artifacts: 'build_test/*BloodPressureTracker*', allowEmptyArchive: true
      }
    }

    stage('Target') {
      steps {
        script {
          def userInput = input(
            message: 'Please provide the IP address for deployment:',
            parameters: [
              string(name: 'IP_ADDRESS', defaultValue: '127.0.0.1', description: 'Enter the target IP address')
            ]
          )
          env.IP_ADDRESS = userInput
        }
      }
    }

    stage('Deploy') {
      steps {
        echo "Deploying to IP: ${env.IP_ADDRESS}"
      }
    }
  }
}
