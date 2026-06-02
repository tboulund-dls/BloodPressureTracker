var config = require('./dbconfig');
const sql = require('mssql');

async function getALLPatients() {
    try {
        let pool = await sql.connect(config);
        let products = await pool.request().query("SELECT * from Patients");
        return products.recordsets;
    }
    catch (error) {
        console.log(error);
    }
}

async function getPatientById(id) {
    try {
        let pool = await sql.connect(config);
        let data = await pool.request()
            .input('input_parameter', sql.Int, id).query("SELECT * from Patients where SSN = @input_parameter");
        return data.recordsets;
    }
    catch (error) {
        console.log(error);
    }
}

/*async function getALLMeasurements() {
    try {
        let pool = await sql.connect(config);
        let products = await pool.request().query("SELECT * from Measurements");
        return products.recordsets;
    }
    catch (error) {
        console.log(error);
    }
}*/
async function getALLMeasurementsById(id) {
    try {
        let pool = await sql.connect(config);
        let data = await pool.request()
            .input('input_parameter', sql.Int, id).query("SELECT * from Measurements where patientSSN = @input_parameter");
        return data.recordsets;
    }
    catch (error) {
        console.log(error);
    }
}

async function addMeasurement(measurement) {
    try {
        let pool = await sql.connect(config);
        let InsertMeasurement = await pool.request()
        .input('measurementDate', measurement.measurementDate)
        .input('systolic', sql.Int, measurement.systolic)
        .input('diastolic', sql.Int, measurement.diastolic)
        .input('patientSSN', sql.NVarChar, measurement.patientSSN)
        .query("INSERT INTO Measurements (measurementDate, systolic, diastolic, patientSSN) VALUES(@measurementDate, @systolic, @diastolic, @patientSSN)");
        return InsertMeasurement.recordsets;
    }
    catch (err) {
        console.log(err);
    }
}

module.exports = {
    getALLPatients: getALLPatients,
    getPatientById: getPatientById,
    //getALLMeasurements: getALLMeasurements,
    getALLMeasurementsById: getALLMeasurementsById,
    addMeasurement: addMeasurement
}