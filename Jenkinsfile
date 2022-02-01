node('docker') {

    stage 'Checkout'
        checkout scm
    stage 'Build & UnitTest'
        sh "docker build -t mvcapp:B${BUILD_NUMBER} -f Dockerfile ."
        sh "docker build -t mvcapp:test-B${BUILD_NUMBER} -f Dockerfile.Integration ."
  
    stage 'Integration Test'
        sh "docker-compose -f docker-compose.integration.yaml up --force-recreate --abort-on-container-exit"
        sh "docker-compose -f docker-compose.integration.yaml down -v"
}