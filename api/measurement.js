class measurement {
    constructor(measurementDate, systolic, diastolic, patientSSN) {
        this.measurementDate = measurementDate;
        this.systolic = systolic;
        this.diastolic = diastolic;
        this.patientSSN = patientSSN;
    }
}

module.exports = measurement;