import React from 'react';
import Style from "./Rooms.module.css";
import axios from 'axios';
import { useQuery, useQueries } from "react-query";

const fetchHomeData = (homeId) => {
    return axios.get(`http://localhost:62863/api/Home/home-with-rooms-user/${homeId}`);
};

const fetchTempAndHumidity = (roomId) => {
    return axios.post(`http://localhost:62863/api/temp_humidity/last/${roomId}`);
};

const fetchEnergyConsumption = (roomId, energyType) => {
    return axios.get(`http://localhost:62863/api/room_overall/last/${roomId}`, {
        params: { energyType }
    });
};

const useRoomData = (homeId) => {
    const homeDataQuery = useQuery(['homeData', homeId], () => fetchHomeData(homeId));

    const roomQueries = useQueries(
        (homeDataQuery.data?.data.rooms || []).map(room => ({
            queryKey: ['roomData', room.id],
            queryFn: async () => {
                const [tempHumidity, electricity, gas] = await Promise.all([
                    fetchTempAndHumidity(room.id),
                    fetchEnergyConsumption(room.id, 'Electricity'),
                    room.type === 'Kitchen' ? fetchEnergyConsumption(room.id, 'gas') : Promise.resolve(null)
                ]);

                return {
                    room,
                    tempHumidity: tempHumidity.data,
                    electricity: electricity.data,
                    gas: gas ? gas.data : null,
                };
            },
            enabled: homeDataQuery.isSuccess,
            refetchInterval: 30000,
        }))
    );

    return { homeDataQuery, roomQueries };
};

export default function Rooms() {
    const homeId = parseInt(localStorage.getItem("homeid"));
    const { homeDataQuery, roomQueries } = useRoomData(homeId);

    return (
        <div id='cards' className={`${Style.cards} container`}>
            <div className='row'>
                {roomQueries.map(({ data, isLoading, isError }) => {
                    if (isLoading) return <div key={data?.room.id} className="col-12 loading">Loading...</div>;
                    if (isError) return <div key={data?.room.id} className="col-12 error">Error loading data</div>;

                    const { room, tempHumidity, electricity, gas } = data;

                    return (
                        <div key={room.id} className='col-12 col-md-6 mb-4'>
                            <div className={`${Style.card} p-3`}>
                                <div className={`${Style.cardInner}`}>
                                    <h2 className='h4'>Indoor Temperature</h2>
                                    <span className={`${Style.span}`}><i className="fa-solid fa-house me-3"></i></span>
                                </div>
                                <h1 className='display-4'>{tempHumidity?.temperature} <span className='fs-4'>C</span></h1>
                            </div>
                            <div className={`${Style.card} p-3 mt-3`}>
                                <div className={`${Style.cardInner}`}>
                                    <h2 className='h4'>Humidity</h2>
                                    <span className={`${Style.span}`}><i className="fa-solid fa-droplet me-3"></i></span>
                                </div>
                                <h1 className='display-4'>{tempHumidity?.humidity} <span className='fs-4'>%</span></h1>
                            </div>
                            <div className={`${Style.card} p-3 mt-3`}>
                                <div className={`${Style.cardInner}`}>
                                    <h2 className='h4'>Electricity Consumption</h2>
                                    <span className={`${Style.span}`}><i className="fa-solid fa-bolt me-3"></i></span>
                                </div>
                                <h1 className='display-4'>{electricity?.roomConsumption} <span className='fs-4'>Kwh</span></h1>
                            </div>
                            {room.type === 'Kitchen' && (
                                <div className={`${Style.card} p-3 mt-3`}>
                                    <div className={`${Style.cardInner}`}>
                                        <h2 className='h4'>Gas Consumption</h2>
                                        <span className={`${Style.span}`}><i className="fa-solid fa-fire me-3"></i></span>
                                    </div>
                                    <h1 className='display-4'>{gas?.roomConsumption} <span className='fs-4'>CF</span></h1>
                                </div>
                            )}
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
