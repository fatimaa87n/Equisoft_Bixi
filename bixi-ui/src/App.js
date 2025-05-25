import logo from './bixi-logo.png';
import './App.css';
import React, { useState, useEffect } from 'react';
import StationList from './components/StationList';
import { fetchStations } from './services/stationService';
import  Button  from 'react-bootstrap/Button';

const App = () => {
    const [queryParams, setQueryParams] = useState({
        Name: '',
        MinAvailableBikes: '',
        MaxAvailableBikes: '',
        MinAvailableDocks: '',
        MaxAvailableDocks: '',
        hasEbike: false,
        SortBy: 'Name',
        SortOrder: 'asc'
});
    const [stations, setStations] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [loading, setLoading] = useState(true);

    const pageSize = 10;

    useEffect(() => {
        setLoading(true);
        fetchStations(
            queryParams
        ).then(data => {
            setStations(data);
            setLoading(false);
        });
    }, [queryParams]);


    const updateQuery = (key, value) => {
        setQueryParams(prev => ({
            ...prev,
            [key]: value,
         
        }));
        setCurrentPage(1);
    };
    const handleClearFilter = () => {
        setCurrentPage(1);
        setQueryParams({
            Name: '',
            MinAvailableBikes: '',
            MaxAvailableBikes: '',
            MinAvailableDocks: '',
            MaxAvailableDocks: '',
            hasEbike: false,
            SortBy: 'stationName',
            SortOrder: 'asc'
        })
    }
    const handleSort = (column) => {
        setQueryParams(prev => ({
            ...prev,
            SortBy: column,
            SortOrder: prev.SortBy === column && prev.SortOrder === 'asc' ? 'desc' : 'asc'
        }));
    };

    
  return (
    <div className="App">
      <header className="App-header" >
              <div className="logo-wrapper">
                  <a className="logo" >
                      <img width="86" height="24" src={logo} class="" alt="" decoding="async" ></img>
                    
                  </a>
              </div>
          </header>
          <div className="container mt-5">
          <h1 className="listTitle ">Station List</h1>
              {/* Filters */}
              <div className="row mb-3">
                  <div className="col-md-4">
                      <input
                          className="form-control"
                          placeholder="Station Name"
                          value={queryParams.Name}
                          onChange={(e) => updateQuery('Name', e.target.value)}
                      />
                  </div>
                  <div className="col-md-4">
                      <input type="number" placeholder="Minimum Bikes Available"  className="form-control" id="bikeMin"
                              onChange={(e) => updateQuery('MinAvailableBikes', e.target.value)} value={queryParams.MinAvailableBikes}></input>
                  </div>
                  <div className="col-md-4">
                      <input type="number" className="form-control" id="dockMin"
                          placeholder="Minimum Docks Available"
                          value={queryParams.MinAvailableDocks}
                          onChange={(e) => updateQuery('MinAvailableDocks', e.target.value)}></input>
                  </div>
              </div>
              <div className="row mb-3">

                  <div className="col-md-8 ">
                      <label className="form-check-label" >E-Bike</label>
                      <input className="form-check-input ms-2" type="checkbox"  id="hasEbike" checked={queryParams.hasEbike}
                          onChange={(e) => updateQuery('hasEbike', e.target.checked)}></input>
                        </div>
                  <div className="col-md-4 d-flex justify-content-end">
                      <Button  onClick={handleClearFilter} variant="primary">Clear</Button>
                  </div>

              </div>
              {loading ? <div className="text-center mt-5">
                  <div className="spinner-border text-primary" role="status">
                      <span className="visually-hidden">Loading...</span>
                  </div>
              </div>

                  : <StationList
                      stations={stations}
                      currentPage={currentPage}
                      setPage={setCurrentPage}
                      onSort={handleSort}
                      sortBy={queryParams.SortBy}
                      sortOrder={queryParams.SortOrder}
                  ></StationList>}

              
          </div>
    </div>
  );
}

export default App;
