"use client";

import React, { useEffect, useState } from "react";
import { useRouter, useParams } from "next/navigation";
import ToolDetails from "@/components/ToolDetails";
import { ApiService } from "@/services/api";
import { Tool } from "@/types";

export default function ToolDetailPage() {
  const router = useRouter();
  const params = useParams();
  const { id } = params as { id: string };
  const [tool, setTool] = useState<Tool | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    ApiService.getTool(Number(id))
      .then(setTool)
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div style={{ padding: 40 }}>Loading...</div>;
  if (!tool) return <div style={{ padding: 40 }}>Tool not found.</div>;

  return (
    <ToolDetails
      tool={tool}
      onBack={() => router.push("/")}
      onReviewAdded={async () => {
        const updated = await ApiService.getTool(Number(id));
        setTool(updated);
      }}
    />
  );
}
