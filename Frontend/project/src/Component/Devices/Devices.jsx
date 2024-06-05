import React from 'react';
import Style from "./Devices.module.css";
import axios from 'axios';
import { useQueries, useQuery } from "react-query";

const fetchHomeData = (homeId) => {
    return axios.get(`http://localhost:62863/api/Home/home-with-rooms-user/${homeId}`);
};

const fetchRoomDetails = (roomId) => {
    return axios.get(`http://localhost:62863/api/Room/RoomDetalis/${roomId}`);
};

const fetchApplianceData = (deviceId, energyType) => {
    return axios.get(`http://localhost:62863/api/appliance/last/${deviceId}`, {
        params: { energyType }
    });
};

const useDeviceData = (homeId) => {
    const homeDataQuery = useQuery(['homeData', homeId], () => fetchHomeData(homeId));

    const roomQueries = useQueries(
        (homeDataQuery.data?.data.rooms || []).map(room => ({
            queryKey: ['roomData', room.id],
            queryFn: async () => {
                const roomDetails = await fetchRoomDetails(room.id);
                const deviceQueries = await Promise.all(
                    roomDetails.data.devices.map(device =>
                        fetchApplianceData(device.id, device.energyType).then(response => ({
                            device,
                            applianceData: response.data
                        }))
                    )
                );
                return {
                    room,
                    devices: deviceQueries
                };
            },
            enabled: homeDataQuery.isSuccess
        }))
    );

    return { homeDataQuery, roomQueries };
};

export default function Devices() {
    const homeId = parseInt(localStorage.getItem("homeid"));
    const { homeDataQuery, roomQueries } = useDeviceData(homeId);

    if (homeDataQuery.isLoading) return <div className="loading">Loading...</div>;
    if (homeDataQuery.isError) return <div className="error">Error loading data</div>;

    return (
        <div id='cards' className={`${Style.cards} container`}>
            <div className='row'>
                {roomQueries.map(({ data, isLoading, isError }) => {
                    if (isLoading) return <div key={data?.room.id} className="col-12 loading">Loading...</div>;
                    if (isError) return <div key={data?.room.id} className="col-12 error">Error loading data</div>;

                    const { room, devices } = data;

                    return (
                        <div key={room.id} className='col-12 col-md-6 mb-4'>
                            <div className={`${Style.card}`}>
                                <div className={`${Style.cardHeader}`}>
                                    {room.type}
                                </div>
                                <div className="p-3">
                                    {devices.map(({ device, applianceData }) => (
                                        <div key={device.id} className={`${Style.cardInner} mb-3`}>
                                            <div className={`${Style.deviceLabel}`}>
                                                Device {device.id}
                                            </div>
                                            <div className={`${Style.cardInner}`}>
                                                <h2>{device.energyType} Consumption </h2>
                                                <span className={`${Style.span}`}>
                                                    <i className={`fa-solid fa-${device.energyType.toLowerCase() === 'electricity' ? 'bolt' : 'fire'} me-3`}></i>
                                                </span>
                                            </div>
                                            <h1 className='display-4'>{applianceData.applianceConsumption} <span className='fs-4'>{device.energyType === 'Electricity' ? 'Kwh' : 'CF'}</span></h1>
                                        </div>
                                    ))}
                                </div>
                            </div>
                        </div>
                    );
                })}
            </div>
            <div className='col-12'>
                {/* Chart component can be placed here */}
            </div>
        </div>
    );
}
