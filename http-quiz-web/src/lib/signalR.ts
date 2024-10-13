"use client";

import { HubConnectionBuilder } from "@microsoft/signalr";

export const getSignalR = (url: string) =>
  new HubConnectionBuilder()
    .withUrl(`${url}/hub`)
    .withAutomaticReconnect()
    .build();
