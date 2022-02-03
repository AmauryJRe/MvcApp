node() {

    stage 'Slack Notification'
        slackSend color: "#439FE0", message: "Build Started - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<${env.BUILD_URL}|Open>)"
    
    stage 'Checkout'
        checkout scm
    
    stage 'Build & UnitTest'
        sh "docker build -t mvcapp:B${BUILD_NUMBER} -f Dockerfile ."
        // sh "docker build -t mvcapp:test-B${BUILD_NUMBER} -f Dockerfile.Integration ."
    
    stage 'Pusblish UT Reports'
        containerID = sh (
            script: "docker run -d mvcapp:B${BUILD_NUMBER}", 
        returnStdout: true
        ).trim()
        echo "Container ID is ==> ${containerID}"
        sh "docker cp ${containerID}:/TestResults/test_results.xml test_results.xml"
        sh "docker stop ${containerID}"
        sh "docker rm ${containerID}"
        step([$class: 'MSTestPublisher', failOnError: false, testResultsFile: 'test_results.xml'])

    // stage 'Integration Test'
    //     sh "docker-compose -f docker-compose.integration.yaml up --force-recreate --abort-on-container-exit"
    //     sh "docker-compose -f docker-compose.integration.yaml down -v"
}