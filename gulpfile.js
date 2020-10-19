let preprocessor = 'sass'
const { src,dest,parallel,series,watch } = require('gulp');
const concat = require('gulp-concat');
// const uglify = require('gulp-uglify-es').default;
const sass = require('gulp-sass');
const autoprefixer = require('gulp-autoprefixer');
const cleancss = require('gulp-clean-css');


function styles() {
  return src('wwwroot/style/*.scss')
    .pipe(eval(preprocessor)())
    .pipe(concat('app.min.css'))
    .pipe(autoprefixer({ overrideBrowserslist: ['last 10 versions'],grid: true }))
    .pipe(cleancss({ level: { 1: { specialComments: 0 } },format: 'beautify' }))
    .pipe(dest('wwwroot/css'))
}

function startwatch() {
  watch('wwwroot/style/*.scss', styles);
}

exports.styles = styles;
exports.default = parallel(styles, startwatch);