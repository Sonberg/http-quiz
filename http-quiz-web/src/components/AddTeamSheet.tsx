"use client";

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Sheet,
  SheetClose,
  SheetContent,
  SheetDescription,
  SheetFooter,
  SheetHeader,
  SheetTitle,
} from "@/components/ui/sheet";
import { Api } from "@/lib/api";
import { useState } from "react";
import { z } from "zod";

const schema = z.object({
  name: z.string(),
  port: z.string(),
});

type Props = {
  ipAddress: string;
};

export function AddTeamSheet({ ipAddress }: Props) {
  const [name, setName] = useState("");
  const [port, setPort] = useState("");
  const [isOpen, setOpen] = useState(false);

  const submit = async () => {
    const parsed = await schema.safeParseAsync({ name, port });

    if (!parsed.success) {
      throw new Error(parsed.error.message);
    }

    await Api.post("/api/add-team", {
      name: parsed.data.name,
      baseUrl: `http://${ipAddress}:${port}`,
    });

    setName("");
    setPort("");
    setOpen(false);
  };

  return (
    <Sheet open={isOpen}>
      <Button className="text-xs px-2 py-2 mt-8" onClick={() => setOpen(true)}>
        Add team
      </Button>
      <SheetContent onClose={() => setOpen(false)}>
        <SheetHeader>
          <SheetTitle>Add your team</SheetTitle>
          <SheetDescription>
            Select a cool team name and let us know on which port your api is running on. We will use your current IP = <strong>{ipAddress}</strong>
          </SheetDescription>
        </SheetHeader>
        <div className="grid gap-8 py-4">
          <div className="gap-4 flex flex-col">
            <Label htmlFor="name" className="">
              Name
            </Label>
            <Input
              id="name"
              value={name}
              placeholder="Your best team name!"
              onChange={(ev) => setName(ev.target.value)}
              className="col-span-3"
            />
          </div>
          <div className="gap-4 flex flex-col">
            <Label htmlFor="port">Port</Label>
            <Input
              id="port"
              value={port}
              onChange={(ev) => setPort(ev.target.value)}
              placeholder="Which your api is running on"
              className="col-span-3"
            />
          </div>
        </div>
        <SheetFooter>
          <SheetClose asChild>
            <Button onClick={submit}>Add</Button>
          </SheetClose>
        </SheetFooter>
      </SheetContent>
    </Sheet>
  );
}
