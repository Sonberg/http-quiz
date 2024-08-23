"use client";

import { HubConnectionBuilder } from "@microsoft/signalr";

export const SignalR = new HubConnectionBuilder()
  .withUrl(`${process.env.NEXT_PUBLIC_API_URL}/hub`)
  .withAutomaticReconnect()
  .build();
