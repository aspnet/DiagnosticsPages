
module.exports = function (grunt) {

    grunt.initConfig({
        jshint: {
            src: [
                "gruntfile.js",
                "src/**/*.js"
            ],
            options: {
                // Options are documented at https://github.com/gruntjs/grunt-contrib-jshint#options
                jshintrc: ".jshintrc"
            }
        },
        csslint: {
            src: [
                "src/**/*.css"
            ],
            options: {
                // Options are documented at https://github.com/gruntjs/grunt-contrib-csslint#options
                csslintrc: ".csslintrc"
            }
        }
    });

    grunt.loadNpmTasks("grunt-contrib-jshint");
    grunt.loadNpmTasks("grunt-contrib-csslint");


    grunt.registerTask("default", ["jshint", "csslint"]);
};