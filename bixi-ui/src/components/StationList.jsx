import React, { useState } from 'react';
import { Pagination } from 'react-bootstrap';

const StationList = ({ stations, currentPage, setPage, onSort, sortBy, sortOrder }) => {

    const pageSize = 10;
    const totalPages = Math.ceil(stations.length / pageSize);
    const handleClick = (pageNumber) => {
        setPage(pageNumber);
    };

    const currentItems = stations.slice(
        (currentPage - 1) * pageSize,
        currentPage * pageSize
    );
    const sortIcon = (column) => {
        if (sortBy !== column) return null;
        return sortOrder === "asc" ? "▲" : "▼";
    };
    const renderPaginationItems = () => {
        const items = [];

        // Show at most 5 page numbers around current page
        const start = Math.max(1, currentPage - 2);
        const end = Math.min(totalPages, currentPage + 2);

        if (start > 1) items.push(<Pagination.Ellipsis key="start-ellipsis" disabled />);

        for (let number = start; number <= end; number++) {
            items.push(
                <Pagination.Item
                    key={number}
                    active={number === currentPage}
                    onClick={() => handleClick(number)}
                >
                    {number}
                </Pagination.Item>
            );
        }

        if (end < totalPages) items.push(<Pagination.Ellipsis key="end-ellipsis" disabled />);

        return items;
    };
    return (
        <>
        <table className="table table-striped table-bordered">
            <thead>
                    <tr>
               
                        {<th>Item</th>}
                    <th onClick={() => onSort("stationName")}>Station Name {sortIcon("stationName")}</th>
                    <th onClick={() => onSort("availableBikes")}>Available Bikes {sortIcon("availableBikes")}</th>
                    <th onClick={() => onSort("availableDocks")}>Available Docks {sortIcon("availableDocks")}</th>
                    <th onClick={() => onSort("availabilityPercentage")}>Availability % {sortIcon("availabilityPercentage")}</th>
                    <th onClick={() => onSort("lastUpdatedTime")}>Last Updated {sortIcon("lastUpdatedTime")}</th>
                </tr>
            </thead>
                <tbody>
                    {currentItems.map((station,index) => (
                        <tr key={station.id}>
                            <td>{(currentPage - 1) * pageSize + index + 1}</td>
                        <td>{station.name}</td>
                        <td>{station.availableBikesNum}</td>
                        <td>{station.availableDocksNum}</td>
                        <td>{station.bikeAvailabilityPercentage}%</td>
                        <td>{new Date(station.lastReported).toLocaleString()}</td>
                    </tr>
                ))}
            </tbody>
        </table>
        <Pagination>
        <Pagination.First onClick={() => handleClick(1)} disabled={currentPage === 1} />
        <Pagination.Prev onClick={() => handleClick(currentPage - 1)} disabled={currentPage === 1} />
        {renderPaginationItems()}
        <Pagination.Next onClick={() => handleClick(currentPage + 1)} disabled={currentPage === totalPages} />
        <Pagination.Last onClick={() => handleClick(totalPages)} disabled={currentPage === totalPages} />
      </Pagination>
      </>
    );
}

export default StationList;
