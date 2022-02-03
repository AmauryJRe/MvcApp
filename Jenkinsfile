node() {

    // stage 'Slack Start Notification'

    stage 'Checkout'
        checkout scm
        slackNotification('STARTED')

    stage 'Build & UnitTest'
        sh "docker build -t mvcapp:B${BUILD_NUMBER} -f Dockerfile ."
        slackNotification(currentBuild.result)
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
        slackNotification(currentBuild.result)

    // stage 'Slack Result Notification'
    //     slackNotification(currentBuild.result)

    // stage 'Integration Test'
    //     sh "docker-compose -f docker-compose.integration.yaml up --force-recreate --abort-on-container-exit"
    //     sh "docker-compose -f docker-compose.integration.yaml down -v"

}
def slackNotification(String buildStatus){
    buildStatus =  buildStatus ?: 'SUCCESSFUL'

    // Default values
    String color = 'RED'
    String colorCode = '#FF0000'
    String subject = "${buildStatus}: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'"
    String summary = "${subject} (${env.BUILD_URL})"

    // Override default values based on build status
    if (buildStatus == 'STARTED') {
        color = 'YELLOW'
        colorCode = '#FFFF00'
    } else if (buildStatus == 'SUCCESSFUL') {
        color = 'GREEN'
        colorCode = '#00FF00'
    } else {
        color = 'RED'
        colorCode = '#FF0000'
    }

    // Send notifications
    slackSend (color: colorCode, message: summary)
}
