var Db = require('./dboperations');
var Patient = require('./patient');
var Measurement = require('./measurement');
var express = require('express');
var bodyParser = require('body-parser');
var cors = require('cors');
var app = express();
var router = express.Router();

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use(cors());
app.use('/api', router);

var port = process.env.PORT || 8090;
app.listen(port);
console.log('patient API is runnning at ' + port);

router.use((request, response, next) => {
    console.log('middleware');
    next();
});

router.route('/patient').get((request, response) => {
    Db.getALLPatients().then((data) => {
        response.json(data[0]);
    })
});

router.route('/patient/:id').get((request, response) => {
    Db.getPatientById(request.params.id).then((data) => {
        response.json(data[0]);
    })
});

/*router.route('/measurements').get((request, response) => {
    Db.getALLMeasurements().then((data) => {
        response.json(data[0]);
    })
});*/
router.route('/measurements/:id').get((request, response) => {
    Db.getALLMeasurementsById(request.params.id).then((data) => {
        if(data) {
        response.json(data[0]);}
    })
});
router.route('/measurements').post((request, response) => {
    let measurement = { ...request.body }
    console.log(measurement);
    Db.addMeasurement(measurement).then(data => {
        response.status(201).json(data);
    })
})