const  config = {
    user:  'sa', // sql user
    password:  'yourStrong(!)Password', //sql user password
    server:  'localhost', // if it does not work try- localhost
    database:  'BPT',
    options: {
      trustedconnection:  true,
      trustServerCertificate: true,
      enableArithAbort:  true,
      instancename:  'sqlserver'  // SQL Server instance name
    },
    port:  1433
  }
  
  module.exports = config;