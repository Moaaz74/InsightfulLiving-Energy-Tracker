import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Style from './History.module.css';
import { Chart as ChartJS, LineElement, PointElement, LinearScale, CategoryScale, Tooltip, Legend } from 'chart.js';
import { Line } from 'react-chartjs-2';

export default function History() {
    const homeId = parseInt(localStorage.getItem("homeid"), 10);

    const [energyType, setEnergyType] = useState('Electricity');
    const [startDates, setStartDates] = useState([]);
    const [endDates, setEndDates] = useState([]);
    const [selectedStartDate, setSelectedStartDate] = useState('');
    const [selectedEndDate, setSelectedEndDate] = useState('');
    const [chartData, setChartData] = useState({ labels: [], datasets: [] });

    useEffect(() => {
        const fetchStartDates = async () => {
            try {
                const response = await axios.get(`http://localhost:62863/api/home_overall/startDates/${homeId}`, {
                    params: { energyType }
                });
                setStartDates(response.data);
            } catch (error) {
                console.error('Error fetching start dates:', error);
            }
        };
        fetchStartDates();
    }, [energyType, homeId]);

    useEffect(() => {
        if (!selectedStartDate) return;

        const fetchEndDates = async () => {
            try {
                const response = await axios.get(`http://localhost:62863/api/home_overall/endDates/${homeId}`, {
                    params: { energyType, startDate: selectedStartDate }
                });
                setEndDates(response.data);
            } catch (error) {
                console.error('Error fetching end dates:', error);
            }
        };
        fetchEndDates();
    }, [selectedStartDate, energyType, homeId]);

    const fetchChartData = async () => {
        try {
            const response = await axios.get(`http://localhost:62863/api/home_overall/data/${homeId}`, {
                params: {
                    energyType,
                    startDate: selectedStartDate,
                    endDate: selectedEndDate
                }
            });
            const data = response.data;
            console.log(data);
            const formattedData = formatChartData(data);
            setChartData(formattedData);
        } catch (error) {
            console.error('Error fetching chart data:', error);
        }
    };

    ChartJS.register(LineElement, PointElement, LinearScale, CategoryScale, Tooltip, Legend);

    const formatChartData = (apiData) => {
        const sortedData = apiData.sort((a, b) => new Date(b.end) - new Date(a.end));
        const labels = sortedData.map(item => item.end);
        const homeConsumptionData = sortedData.map(item => item.homeconsumption);

        return {
            labels,
            datasets: [
                {
                    label: 'Home Consumption',
                    data: homeConsumptionData,
                    borderColor: 'rgb(54, 162, 235)',
                    backgroundColor: 'rgba(54, 162, 235, 0.5)',
                }
            ]
        };
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        fetchChartData();
    };

    return (
        <div className='d-flex flex-column ms-5 py-5'>
            <h3 className='h1 mb-5'>Show Your History Consumption</h3>
            <form className='d-flex flex-column' onSubmit={handleSubmit}>
                <label htmlFor="energyType" className='h4 fw-bolder'>Energy Type:</label>
                <select id="energyType" className="form-control w-75 mb-5 p-3" value={energyType} onChange={(e) => setEnergyType(e.target.value)}>
                    <option value="Electricity">Electricity</option>
                    <option value="Gas">Gas</option>
                </select>
                <label htmlFor="StartDate" className='h4 fw-bolder'>Start Date:</label>
                <select id="StartDate" className="form-control w-75 mb-5 p-3" value={selectedStartDate} onChange={(e) => setSelectedStartDate(e.target.value)}>
                    <option value="">Select Start Date</option>
                    {startDates.map((date) => (
                        <option key={date} value={date}>{date}</option>
                    ))}
                </select>
                <label htmlFor="EndDate" className='h4 fw-bolder'>End Date:</label>
                <select id="EndDate" className="form-control w-75 mb-5 p-3" value={selectedEndDate} onChange={(e) => setSelectedEndDate(e.target.value)}>
                    <option value="">Select End Date</option>
                    {endDates.map((date) => (
                        <option key={date} value={date}>{date}</option>
                    ))}
                </select>
                <button className='btn btn-success w-25 p-2 fw-bolder' type="submit">Submit</button>
            </form>
            <div className='w-100 mt-3'>
                <div className={Style.Line}>
                    <Line data={chartData} />
                </div>
            </div>
        </div>
    );
}
