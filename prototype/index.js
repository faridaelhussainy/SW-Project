const express = require('express');
const mysql = require('mysql2');
const bodyParser = require('body-parser');

// Initialize Express app
const app = express();
const port = 3000;

// Middleware
app.use(bodyParser.json());

// MySQL Connection
const db = mysql.createConnection({
  host: 'localhost',
  user: 'root', // Replace with your MySQL username
  password: '1234', // Replace with your MySQL password
  database: 'software', // Replace with your database name
});

// Connect to MySQL
db.connect((err) => {
  if (err) {
    console.error('Error connecting to MySQL:', err);
    return;
  }
  console.log('Connected to MySQL');
});

// Routes
// Home Route
app.get('/', (req, res) => {
  res.send('Welcome to the Express Server with MySQL!');
});

// Get All Records
app.get('/users', (req, res) => {
  const sql = 'SELECT * FROM software.users';
  db.query(sql, (err, results) => {
    if (err) {
      console.error(err);
      res.status(500).send('Error retrieving users');
    } else {
      res.json(results);
    }
  });
});

app.get('/appointment', (req, res) => {
    const sql = 'SELECT * FROM software.appointment';
    db.query(sql, (err, results) => {
      if (err) {
        console.error(err);
        res.status(500).send('Error retrieving appointment');
      } else {
        res.json(results);
      }
    });
  });

  app.get('/patient', (req, res) => {
    const sql = 'SELECT * FROM software.patient';
    db.query(sql, (err, results) => {
      if (err) {
        console.error(err);
        res.status(500).send('Error retrieving patient');
      } else {
        res.json(results); 
      }
    });
  });

// Insert a Record 
app.post('/users', (req, res) => {
  const { Username , PasswordHash } = req.body;
  const sql = 'INSERT INTO software.users  ( Username , PasswordHash) VALUES (?,? )';
  db.query(sql, [ Username , PasswordHash], (err, result) => {
    if (err) {
      console.error(err);
      res.status(500).send('Error inserting record');
    } else {
      res.json({ id: result.id, Username , PasswordHash});
    }
  });
}); 

app.post('/appointment', (req, res) => {
    const { Patientid, doctorid } = req.body;
    const sql = 'INSERT INTO software.appointment  (Patientid, doctorid) VALUES (?,? )';
    db.query(sql, [Patientid, doctorid], (err, result) => {
      if (err) {
        console.error(err);
        res.status(500).send('Error inserting record');
      } else {
        res.json({ id: result.id, Patientid, doctorid });
      }
    });
  });

  app.post('/patient', (req, res) => {
    const { Name , gender , diseases , Height , Weight , Age , bloodtype  } = req.body;
    const sql = 'INSERT INTO software.patient  (Name , gender , diseases , Height , Weight , Age , bloodtype) VALUES (?,?,?,?,?,?,? )';
    db.query(sql, [Name , gender , diseases , Height , Weight , Age , bloodtype ], (err, result) => {
      if (err) {
        console.error(err);
        res.status(500).send('Error inserting record');
      } else {
        res.json({ id: result.id,Name , gender , diseases , Height , Weight , Age , bloodtype });
      }
    });
  });

// Start Server
app.listen(port, () => {
  console.log(`Server running on http://localhost:${port}`);
}); 