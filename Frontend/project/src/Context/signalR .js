// import { createSignalRContext } from "react-signalr/signalr";

// const SignalRContext = createSignalRContext();

// const App = () => {
//   const { token } = YourAccessToken;

//   return (
//     <SignalRContext.Provider
//       connectEnabled={!!token}
//       accessTokenFactory={() => token}
//       dependencies={[token]} //remove previous connection and create a new connection if changed
//       url={"https://example/hub"}
//     >
//       <Routes />
//     </SignalRContext.Provider>
//   );
// };